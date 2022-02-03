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
            builder.ToTable("Enterprise");

            builder.HasKey("TIN");
        }
    }
}
