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
    [Migration("20220302212126_EditAndAddColumnGotItTransResTable")]
    partial class EditAndAddColumnGotItTransResTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.GotItTransactionRequest", b =>
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

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GotItTransactionRequests");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.GotItTransactionResFail", b =>
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

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductId")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GotItTransactionResFails");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.GotItTransactionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("BrandName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherImageLink")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherLink")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherLinkCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("GotItTransactionResponses");
                });

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

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.UrboxTransactionRequest", b =>
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

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UrboxTransactionRequests");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.UrboxTransactionResFail", b =>
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

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductId")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UrboxTransactionResFails");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.UrboxTransactionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CodeDisplay")
                        .HasColumnType("longtext");

                    b.Property<int?>("CodeDisplayType")
                        .HasColumnType("int");

                    b.Property<string>("CodeImage")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("longtext");

                    b.Property<string>("DeliveryNote")
                        .HasColumnType("longtext");

                    b.Property<string>("EstimateDelivery")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Link")
                        .HasColumnType("longtext");

                    b.Property<string>("Payload")
                        .HasColumnType("longtext");

                    b.Property<string>("Pin")
                        .HasColumnType("longtext");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<string>("PropductId")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("UrboxTransactionResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
