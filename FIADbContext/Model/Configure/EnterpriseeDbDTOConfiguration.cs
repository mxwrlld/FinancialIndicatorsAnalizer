using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FIADbContext.Model.DTO;

namespace FIADbContext.Model.Configure
{
    class EnterpriseeDbDTOConfiguration: IEntityTypeConfiguration<EnterpriseDbDTO>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDbDTO> builder)
        {
            builder.HasKey("TIN");

            builder.ToTable("Enterprise");

            builder.Property(enterprise => enterprise.TIN)
                .HasColumnType("nvarchar(10)");

            builder.Property(enterprise => enterprise.Name)
                .HasColumnType("nvarchar(50)")
                .IsRequired();
            
            builder
                .HasOne(enterprise => enterprise.Manager)
                .WithOne(user => user.Enterprise)
                .HasForeignKey<UserDbDTO>(user => user.EntepriseTIN);
        }
    }
}
