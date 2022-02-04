using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using FIAModel;
using FIADbContext.Model;
using ConsoleFIA.DB.Mapping;
using Microsoft.EntityFrameworkCore;


namespace ConsoleFIA.DB
{
    class DbManager
    {
        static string connectionString = (new ConnectionManager()).ConnectionString;

        static FIAContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FIAContext>();
            var options = optionsBuilder
                    .UseSqlServer(connectionString)
                    .Options;
            return new FIAContext(options);
        }

        public static List<Enterprise> GetEnterprises()
        {
            using(var context = CreateContext())
            {
                return context.Enterprises
                    .Include(enterprise => enterprise.FinancialResults)
                    .Select(enterprise => EnterpriseMapper.Map(enterprise))
                    .ToList();
            }
        }

        public static void Updateenterprises(IEnumerable<Enterprise> enterprises)
        {

            using (var context = CreateContext())
            {
                using var transaction = context.Database.BeginTransaction();
                try
                {
                    var enterprisesFromDb = context.Enterprises.AsNoTracking();

                    foreach(var enterpriseFromDb in enterprisesFromDb)
                    {
                        var enterprise = enterprises.FirstOrDefault(entr => entr.TIN.Equals(enterpriseFromDb.TIN));
                        if(enterprise == null)
                        {
                            context.Remove(enterpriseFromDb);
                        }
                        else
                        {
                            context.Update(EnterpriseMapper.Map(enterprise));
                        }
                    }
                    context.SaveChanges();

                    var enterprisesToAdd = enterprises
                        .Where(enterprise => enterprisesFromDb.FirstOrDefault(entr => entr.TIN.Equals(enterprise.TIN)) == null)
                        .Select(entr => EnterpriseMapper.Map(entr))
                        .ToList();
                    context.AddRange(enterprisesToAdd);
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Произошла ошибка при обновлении записей предприятий: " + ex.Message);
                    transaction.Rollback();
                }
            }
        }
    }
}
