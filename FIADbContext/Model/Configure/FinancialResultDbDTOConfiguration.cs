using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIADbContext.Model.DTO;

namespace FIADbContext.Model.Configure
{
    class FinancialResultDbDTOConfiguration: IEntityTypeConfiguration<FinancialResultDbDTO>
    {
        public void Configure(EntityTypeBuilder<FinancialResultDbDTO> builder)
        {
            builder.HasNoKey();

            builder.ToTable("FinancialResult");
        }
    }
}
