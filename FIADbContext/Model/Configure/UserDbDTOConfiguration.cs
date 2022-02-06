using System;
using System.Collections.Generic;
using System.Text;
using FIADbContext.Model.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIADbContext.Model.Configure
{
    class UserDbDTOConfiguration: IEntityTypeConfiguration<UserDbDTO>
    {
        public void Configure(EntityTypeBuilder<UserDbDTO> builder)
        {
            builder.Property(user => user.EntepriseTIN)
                .HasColumnName("Enterprise")
                .HasColumnType("nvarchar(10)");
        }
    }
}
