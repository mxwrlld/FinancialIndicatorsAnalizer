using FIADbContext.Model;
using System;
using System.Collections.Generic;
using System.Text;
using FIADbContext.Model.DTO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FIADbContext.Repositories
{
    public class FinancialResultRepository : FIARepositoryBase
    {
        public FinancialResultRepository(FIAContext context) : base(context)
        {
        }

        public void Create(FinancialResultDbDTO finres)
        {
            context.FinancialResults.Add(finres);
        }

        public void Update(FinancialResultDbDTO finres)
        {
            context.FinancialResults.Update(finres);
        }

        public void Delete(FinancialResultDbDTO finres)
        {
            context.FinancialResults.Remove(finres);
        }

        public async Task<bool> Exists(int year, int quarter, string tin)
        {
            return await context.FinancialResults.AnyAsync(finres => finres.EnterpriseTIN == tin && finres.Year == year && finres.Quarter == quarter);
        }
    }
}
