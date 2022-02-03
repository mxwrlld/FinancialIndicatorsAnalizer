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
            builder.HasKey("Id");

            builder.ToTable("FinancialResult");

            builder.Property(finres => finres.Year)
                .IsRequired();
            builder.Property(finres => finres.Quarter)
                .IsRequired();
            builder.Property(finres => finres.Income)
                .IsRequired();
            builder.Property(finres => finres.Consumption)
                .IsRequired();

            builder.Property(finres => finres.EnterpriseTIN)
                .HasColumnName("Enterprise")
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder
                .HasOne<EnterpriseDbDTO>(finres => finres.Enterprise)
                .WithMany(enterprise => enterprise.FinancialResults)
                .HasForeignKey(finres => finres.EnterpriseTIN);
        }
    }
}
