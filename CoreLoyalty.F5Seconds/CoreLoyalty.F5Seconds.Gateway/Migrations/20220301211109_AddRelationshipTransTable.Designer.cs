﻿// <auto-generated />
using System;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreLoyalty.F5Seconds.Gateway.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220301211109_AddRelationshipTransTable")]
    partial class AddRelationshipTransTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BrandLogo")
                        .HasColumnType("longtext");

                    b.Property<string>("BrandName")
                        .HasColumnType("longtext");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Partner")
                        .HasColumnType("longtext");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.TransactionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerId")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Partner")
                        .HasColumnType("longtext");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<int?>("TransactionResFailId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionResponseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionResFailId");

                    b.HasIndex("TransactionResponseId");

                    b.ToTable("TransactionRequests");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.TransactionResFail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("Partner")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductId")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TransactionResFails");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.TransactionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("longtext");

                    b.Property<string>("ExpiryDate")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TransactionResponses");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.TransactionRequest", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.TransactionResFail", "TransactionResFail")
                        .WithMany()
                        .HasForeignKey("TransactionResFailId");

                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.TransactionResponse", "TransactionResponse")
                        .WithMany()
                        .HasForeignKey("TransactionResponseId");

                    b.Navigation("TransactionResFail");

                    b.Navigation("TransactionResponse");
                });
#pragma warning restore 612, 618
        }
    }
}
