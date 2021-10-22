using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIAModel
{
    public class RegistryOfEnterprises
    {
        public Dictionary<string, Enterprise> Enterprises { get; set; } = new Dictionary<string, Enterprise>(); // Ключ - ИНН

        public void AddEnterprise(Enterprise enterprise)
        {
            Enterprises.Add(enterprise.TIN, enterprise);
        }

        public bool RemoveEnterprise(Enterprise enterprise)
        {
            return Enterprises.Remove(enterprise.TIN);
        }

        public Enterprise FindEnterprise(string name)
        {
            Enterprise foundEnterprise = null;
            
            foreach (var enterprise in Enterprises)
            {
                string enterpriseName = enterprise.Value.Name.ToLower();
                if (enterpriseName == name.ToLower())
                {
                    foundEnterprise = enterprise.Value;
                }
            }
            return foundEnterprise;
        }


        /*
         * При определении ИНН и Адреса в виде 
         * отдельных сущность появится возможность 
         * поиска по упомянутым атрибутам.
         */
    }
}
