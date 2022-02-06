using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FIADbContext.Model;
using FIADbContext.Model.DTO;

namespace FIADbContext.Repositories
{
    public class EnterpriseRepository : FIARepositoryBase
    {
        public EnterpriseRepository(FIAContext context) : base(context) { }

        public async Task<IEnumerable<EnterpriseDbDTO>> GetAllAsync(string search, int? year, int? quarter, string sortBy, bool sortDesc)
        {
            var enterprises = await context.Enterprises
                .Include(enterprise => enterprise.FinancialResults)
                .ToListAsync();

            if(!string.IsNullOrEmpty(search))
            {
                enterprises = enterprises
                    .Where(e => e.Name.ToLower().Contains(search)
                        || e.LegalAddress.ToLower().Contains(search))
                    .ToList();
            }

            if(year != null)
            {
                enterprises = FiltBy(enterprises, (int)year, fr => fr.Year);
            }
            if(quarter != null)
            {
                enterprises = FiltBy(enterprises, (int)quarter, fr => fr.Quarter);
            }

            switch (sortBy)
            {
                case "sumofincome":
                    enterprises = SortBy(sortDesc, enterprises, e => SumOf(e, fr => fr.Income)).ToList();
                    break;
                case "sumofconsumption":
                    enterprises = SortBy(sortDesc, enterprises, e => SumOf(e, fr => fr.Consumption)).ToList();
                    break;
                case "sumofprofit":
                    enterprises = SortBy(sortDesc, enterprises, e => SumOf(e, fr => fr.Income - fr.Consumption)).ToList();
                    break;
                case "sumofrentability":
                    enterprises = SortBy(sortDesc, enterprises, e => SumOf(e, fr => fr.Consumption == 0 
                        ? (double)(fr.Income - fr.Consumption) 
                        : (double)((fr.Income - fr.Consumption) / fr.Consumption)
                    )).ToList();
                    break;
            }

            return enterprises;
        }

        private List<EnterpriseDbDTO> FiltBy(IEnumerable<EnterpriseDbDTO> enterprises, int parameter, Func<FinancialResultDbDTO, int> getParameter)
        {
            var elist = new List<EnterpriseDbDTO>();

            foreach (var e in enterprises)
            {
                var templist = new List<FinancialResultDbDTO>();
                foreach (var fr in e.FinancialResults)
                {
                    if (getParameter(fr) == parameter)
                        templist.Add(fr);
                }
                if (templist.Count != 0)
                {
                    var entr = new EnterpriseDbDTO();
                    entr.TIN = e.TIN;
                    entr.Name = e.Name; 
                    entr.FinancialResults = templist;
                    elist.Add(entr);
                }
            }
            return elist;
        }

        private IEnumerable<EnterpriseDbDTO> SortBy<TKey>(bool isDesc, IEnumerable<EnterpriseDbDTO> enterprises, Func<EnterpriseDbDTO, TKey> expression)
        {
            if (!isDesc)
            {
                return enterprises.OrderBy(expression);
            }
            else
            {
                return enterprises.OrderByDescending(expression);
            }
        }

        private decimal SumOf(EnterpriseDbDTO enterprise,Func<FinancialResultDbDTO, decimal> getParameter)
        {
            decimal sum = 0;
            foreach (var fr in enterprise.FinancialResults)
            {
                sum += getParameter(fr);
            }
            return sum;
        }

        private double SumOf(EnterpriseDbDTO enterprise, Func<FinancialResultDbDTO, double> getParameter)
        {
            double sum = 0;
            foreach (var fr in enterprise.FinancialResults)
            {
                sum += getParameter(fr);
            }
            return sum;
        }

        public async Task<EnterpriseDbDTO> GetAsync(string tin)
        {
            return await context.Enterprises
                .Include(enterprise => enterprise.FinancialResults)
                .FirstOrDefaultAsync(entr => entr.TIN == tin);
        }

        public void Create(EnterpriseDbDTO enterprise)
        {
            context.Enterprises.Add(enterprise);
        }

        public void Update(EnterpriseDbDTO enterprise)
        {
            context.Enterprises.Update(enterprise);
        }

        public void Delete(EnterpriseDbDTO enterprise)
        {
            context.Enterprises.Remove(enterprise);
        }

        public async Task<EnterpriseDbDTO> DeleteAsync(string tin)
        {
            var enterprise = await context.Enterprises.FindAsync(tin);

            if (enterprise != null)
            {
                await context.Entry(enterprise).Collection(e => e.FinancialResults).LoadAsync();
                this.Delete(enterprise);
            }

            return enterprise;
        }

        public async Task<bool> Exists(string tin)
        {
            return await context.Enterprises.AnyAsync(entr => entr.TIN == tin);
        }
    }
}
