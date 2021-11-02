using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FIAModel;
using System.Xml.Serialization;

namespace ConsoleFIA.Files.DTO
{
    [XmlType(TypeName = "Enterprise")]
    public class EnterpriseDTO
    {

        public string Name { get; set; }
        public string TIN { get; set; }
        public string LegalAddress { get; set; }
        public FinancialResultDTO[] FinancialResults { get; set; }

        public static EnterpriseDTO Map(Enterprise enterprise)
        {
            return new EnterpriseDTO()
            {
                Name = enterprise.Name,
                TIN = enterprise.TIN,
                LegalAddress = enterprise.LegalAddress,
                FinancialResults = enterprise.FinancialResults.Select(fr => FinancialResultDTO.Map(fr.Value)).ToArray()
            };
        }

        public static Enterprise Map(EnterpriseDTO enterpriseDTO)
        {
            return new Enterprise(
                enterpriseDTO.Name,
                enterpriseDTO.TIN,
                enterpriseDTO.LegalAddress,
                enterpriseDTO.FinancialResults.Select(fr => FinancialResultDTO.Map(fr)).ToList()
                ) ;
        }

    }
}
