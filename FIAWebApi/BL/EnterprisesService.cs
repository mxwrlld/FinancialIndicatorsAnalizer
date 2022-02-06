using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIADbContext.Repositories;
using FIAWebApi.BL.Model;
using FIAWebApi.BL.Exceprions;
using System.Text.RegularExpressions;

namespace FIAWebApi.BL
{
    public class EnterprisesService
    {
        EnterpriseRepository repository;

        public EnterprisesService(EnterpriseRepository enterpriseRepository)
        {
            this.repository = enterpriseRepository;
        }

        public async Task<IEnumerable<EnterpriseApiDTO>> GetAllAsync(string search, int? year, int? quarter, string sortBy, bool sortDesc)
        {
            var enterprises = await repository.GetAllAsync(search, year, quarter, sortBy, sortDesc);
            return enterprises.Select(entr => new EnterpriseApiDTO(entr));
        }

        public async Task<(EnterpriseApiDTO, Exception)> GetAsync(string tin)
        {
            (var isValid, string msg) = ValidateTIN(tin);
            if (!isValid)
            {
                return (null, new ArgumentException(msg, "tin"));
            }

            var enterprise = await repository.GetAsync(tin);
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
            repository.Create(enterpriseDb);

            try
            {
                await repository.SaveAsync();
            }
            catch (Exception ex)
            {
                if (await repository.Exists(enterprise.TIN))
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

            var enterpriseDb = await repository.GetAsync(tin);

            if (enterpriseDb == null)
            {
                return new KeyNotFoundException($"Предприятие с инн - {tin} не найдено");
            }

            enterprise.Update(enterpriseDb);
            repository.Update(enterpriseDb);

            try
            {
                await repository.SaveAsync();
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

            var enterpriseDb = await repository.DeleteAsync(tin);

            if (enterpriseDb == null)
            {
                return (null, new KeyNotFoundException($"Предприятие с инн - {tin} не найдено"));
            }

            try
            {
                await repository.SaveAsync();
            }
            catch (Exception ex)
            {
                return (null, new SaveChangesException(ex));
            }

            return (new EnterpriseApiDTO(enterpriseDb), null);
        }

        private (bool, string) ValidateTIN(string tin)
        {
            Regex regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(tin) ? (true, null) : (false, "ИНН должен состоять из 10 цифр");
        }
    }
}
