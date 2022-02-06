using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIADbContext.Repositories;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;
using System.Text.RegularExpressions;
using FIADbContext.Model.DTO;
using Microsoft.AspNetCore.Identity;

namespace FIAWebApi.BL
{
    public class EnterprisesService
    {
        EnterpriseRepository enterpriseRepository;
        FinancialResultRepository financialResultRepository;
        UserManager<UserDbDTO> userManager;

        public EnterprisesService(EnterpriseRepository enterpriseRepository, FinancialResultRepository financialResultRepository, UserManager<UserDbDTO> userManager)
        {
            this.enterpriseRepository = enterpriseRepository;
            this.financialResultRepository = financialResultRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<EnterpriseApiDTO>> GetAllAsync(string search, int? year, int? quarter, string sortBy, bool sortDesc)
        {
            var enterprises = await enterpriseRepository.GetAllAsync(search, year, quarter, sortBy, sortDesc);
            return enterprises.Select(entr => new EnterpriseApiDTO(entr));
        }

        public async Task<(EnterpriseApiDTO, Exception)> GetAsync(string tin)
        {
            (var isValid, string msg) = ValidateTIN(tin);
            if (!isValid)
            {
                return (null, new ArgumentException(msg, "tin"));
            }

            var enterprise = await enterpriseRepository.GetAsync(tin);
            if (enterprise == null)
            {
                return (null, new KeyNotFoundException($"Предприятие с инн - {tin} не найдено"));
            }

            return (new EnterpriseApiDTO(enterprise), null);
        }

        public async Task<Exception> CreateAsync(EnterpriseApiDTO enterprise)
        {
            (var isValid, string msg) = ValidateTIN(enterprise.TIN);
            if (!isValid)
            {
                return (new ArgumentException(msg, "tin"));
            }

            var enterpriseDb = enterprise.Create();
            enterpriseRepository.Create(enterpriseDb);

            try
            {
                await enterpriseRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                if (await enterpriseRepository.Exists(enterprise.TIN))
                {
                    return new AlreadyExistsException($"Предприятие с инн - {enterprise.TIN} уже существует!");
                }

                return new SaveChangesException(ex);
            }
            return null;
        }

        public async Task<Exception> UpdateAsync(string tin, EnterpriseApiDTO enterprise)
        {
            (var isValid, string msg) = ValidateTIN(tin);
            if (!isValid)
            {
                return new ArgumentException(msg, "tin");
            }

            var enterpriseDb = await enterpriseRepository.GetAsync(tin);

            if (enterpriseDb == null)
            {
                return new KeyNotFoundException($"Предприятие с инн - {tin} не найдено");
            }

            enterprise.Update(enterpriseDb);
            enterpriseRepository.Update(enterpriseDb);

            try
            {
                await enterpriseRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new SaveChangesException(ex);
            }
            return null;
        }

        public async Task<(EnterpriseApiDTO, Exception)> DeleteAsync(string tin)
        {
            (var isValid, string msg) = ValidateTIN(tin);
            if (!isValid)
            {
                return (null, new ArgumentException(msg, "tin"));
            }

            var enterpriseDb = await enterpriseRepository.DeleteAsync(tin);

            if (enterpriseDb == null)
            {
                return (null, new KeyNotFoundException($"Предприятие с инн - {tin} не найдено"));
            }

            try
            {
                await enterpriseRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return (null, new SaveChangesException(ex));
            }

            return (new EnterpriseApiDTO(enterpriseDb), null);
        }

        public async Task<Exception> AddFinancialResultAsync(string userName, string tin, FinancialResultApiDTO finRes)
        {
            var user = await userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return new Exception();
            }
            if(user.EntepriseTIN != tin)
            {
                return new ArgumentException($"У вас нет доступа к заданному предприятию - {tin}");
            }

            var enterpriseDb = await enterpriseRepository.GetAsync(tin);
            if (enterpriseDb == null)
            {
                return new KeyNotFoundException($"Предприятие с инн - {tin} не найдено");
            }

            if(await financialResultRepository.Exists(finRes.Year, finRes.Quarter, tin))
            {
                return new AlreadyExistsException($"Записи финансовых показателей {finRes.Year} года {finRes.Quarter} квартала для предприятия с инн - {tin} уже существуют");
            }

            var finResDb = finRes.Create();
            finResDb.Enterprise = enterpriseDb;
            finResDb.EnterpriseTIN = enterpriseDb.TIN;
            financialResultRepository.Create(finResDb);
            
            try
            {
                await financialResultRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return new SaveChangesException(ex);
            }
            return null;
        }

        private (bool, string) ValidateTIN(string tin)
        {
            Regex regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(tin) ? (true, null) : (false, "ИНН должен состоять из 10 цифр");
        }
    }
}
