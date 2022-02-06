using System;
using System.Threading.Tasks;
using FIADbContext.Model;

namespace FIADbContext.Repositories
{
    public class FIARepositoryBase
    {
        protected FIAContext context;

        public FIARepositoryBase(FIAContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
