using System;
using System.Collections.Generic;
using System.Text;
using FIAModel;
using System.Xml.Serialization;

namespace ConsoleFIA.Files.DTO
{
    [XmlType(TypeName = "FinancialResult")]
    public class FinancialResultDTO
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public decimal Income { get; set; }
        public decimal Consumption { get; set; }
        public decimal Profit => (Income - Consumption);
        public double Rentability => (double)(Profit / Consumption);

        public static FinancialResultDTO Map(FinancialResult fr)
        {
            return new FinancialResultDTO()
            {
                Year = fr.Year.year,
                Quarter = fr.Quarter,
                Income = fr.Income,
                Consumption = fr.Consumption
            };
        }

        public static FinancialResult Map(FinancialResultDTO frDTO)
        {
            return new FinancialResult(
                frDTO.Year,
                frDTO.Quarter,
                frDTO.Income,
                frDTO.Consumption
                );
        }
    }
}
