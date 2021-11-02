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
            Enterprises.Add(enterprise);
        }

        public bool RemoveEnterprise(Enterprise enterprise)
        {
            return Enterprises.Remove(enterprise);
        }

        public Enterprise FindEnterprise(string name)
        {
            return Enterprises.Find(enteprise => enteprise.Name.Contains(name));
        }

        public List<FinancialResult> GetAllFinancialResults()
        {
            List<FinancialResult> allFinancialResults = new List<FinancialResult>();

            foreach (var enterprise in Enterprises)
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




        /*
         * При определении ИНН и Адреса в виде 
         * отдельных сущность появится возможность 
         * поиска по упомянутым атрибутам.
         */
    }
}
