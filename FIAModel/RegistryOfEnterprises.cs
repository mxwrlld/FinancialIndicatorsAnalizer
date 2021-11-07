using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIAModel
{
    public class RegistryOfEnterprises
    {
        public List<Enterprise> Enterprises { get; set; } = new List<Enterprise>();

        public void AddEnterprise(Enterprise enterprise)
        {
            if (FindEnterprise(enterprise.TIN, entr => entr.TIN) == null)
                Enterprises.Add(enterprise);
            else
                throw new ArgumentException("Элемент с таким ИНН уже существует");

        }

        public bool RemoveEnterprise(Enterprise enterprise)
        {
            return Enterprises.Remove(enterprise);
        }

        public Enterprise FindEnterprise(string name)
        {
            return FindEnterprise(name, enterprise => enterprise.Name);
        }

        public Enterprise FindEnterprise(string searchLine, Func<Enterprise, string> getAtribute)
        {
            return Enterprises.Find(enteprise => getAtribute(enteprise).Contains(searchLine));
        }

        public static List<FinancialResult> GetAllFinancialResults(List<Enterprise> enterprises)
        {
            List<FinancialResult> allFinancialResults = new List<FinancialResult>();

            foreach (var enterprise in enterprises)
            {
                foreach (var fr in enterprise.FinancialResults)
                {
                    allFinancialResults.Add(fr.Value);
                }
            }
            return allFinancialResults;
        }

        public List<Enterprise> FindEnterprises(Year year)
        {
            List<Enterprise> foundFinres = new List<Enterprise>();
            foreach (var entr in Enterprises)
            {
                var enterprise = entr.FindFinancialResults(new Tuple<Year, int>(year, -1));
                if(enterprise.FinancialResults.Count != 0)
                {
                    foundFinres.Add(enterprise);
                }
            }

            return foundFinres;
        }

        public static List<Enterprise> FindEnterprises(List<Enterprise> enterprises, Year year)
        {
            List<Enterprise> foundFinres = new List<Enterprise>();
            foreach (var entr in enterprises)
            {
                var enterprise = entr.FindFinancialResults(new Tuple<Year, int>(year, -1));
                if (enterprise.FinancialResults.Count != 0)
                {
                    foundFinres.Add(enterprise);
                }
            }

            return foundFinres;
        }

        public static List<Enterprise> FindEnterprises(List<Enterprise> enterprises, int quarter)
        {
            List<Enterprise> foundFinres = new List<Enterprise>();
            foreach (var entr in enterprises)
            {
                var enterprise = entr.FindFinancialResults(new Tuple<Year, int>(new Year(-1), quarter));
                if (enterprise.FinancialResults.Count != 0)
                {
                    foundFinres.Add(enterprise);
                }
            }

            return foundFinres;
        }

        public List<Enterprise> FindEnterprises(int quarter)
        {
            List<Enterprise> foundFinres = new List<Enterprise>();
            foreach (var entr in Enterprises)
            {
                var enterprise = entr.FindFinancialResults(new Tuple<Year, int>(new Year(-1), quarter));
                if (enterprise.FinancialResults.Count != 0)
                {
                    foundFinres.Add(enterprise);
                }
            }

            return foundFinres;
        }

    }
}
