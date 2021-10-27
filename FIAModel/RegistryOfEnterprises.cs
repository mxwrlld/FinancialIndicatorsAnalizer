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
            return Enterprises.Find(enteprise => enteprise.Name == name);
        }


        /*
         * При определении ИНН и Адреса в виде 
         * отдельных сущность появится возможность 
         * поиска по упомянутым атрибутам.
         */
    }
}
