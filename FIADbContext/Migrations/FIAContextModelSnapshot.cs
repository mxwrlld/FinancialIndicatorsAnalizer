﻿// <auto-generated />
using FIADbContext.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIADbContext.Migrations
{
    [DbContext(typeof(FIAContext))]
    partial class FIAContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FIADbContext.Model.DTO.EnterpriseDbDTO", b =>
                {
                    b.Property<string>("TIN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LegalAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TIN");

                    b.ToTable("Enterprise");
                });

            modelBuilder.Entity("FIADbContext.Model.DTO.FinancialResultDbDTO", b =>
                {
                    b.Property<decimal>("Consumption")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EnterpriseTIN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quarter")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasIndex("EnterpriseTIN");

                    b.ToTable("FinancialResult");
                });

            modelBuilder.Entity("FIADbContext.Model.DTO.FinancialResultDbDTO", b =>
                {
                    b.HasOne("FIADbContext.Model.DTO.EnterpriseDbDTO", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseTIN");

                    b.Navigation("Enterprise");
                });
#pragma warning restore 612, 618
        }
    }
}
