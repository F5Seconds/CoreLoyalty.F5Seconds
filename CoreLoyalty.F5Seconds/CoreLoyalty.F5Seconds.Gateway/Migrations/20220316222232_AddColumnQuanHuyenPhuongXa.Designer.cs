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
    [Migration("20220316222232_AddColumnQuanHuyenPhuongXa")]
    partial class AddColumnQuanHuyenPhuongXa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.CuaHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("DiaChi")
                        .HasColumnType("longtext");

                    b.Property<string>("DienThoai")
                        .HasColumnType("longtext");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("longtext");

                    b.Property<double>("KinhDo")
                        .HasColumnType("double");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("MoTa")
                        .HasColumnType("longtext");

                    b.Property<string>("NguoiDaiDien")
                        .HasColumnType("longtext");

                    b.Property<int>("PhuongXaId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<double>("ViDo")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PhuongXaId");

                    b.ToTable("CuaHangs");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.LinhVuc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("LinhVucs");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.ThuongHieu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("DiaChi")
                        .HasColumnType("longtext");

                    b.Property<string>("DienThoai")
                        .HasColumnType("longtext");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("MoTa")
                        .HasColumnType("longtext");

                    b.Property<string>("NguoiDaiDien")
                        .HasColumnType("longtext");

                    b.Property<int>("PhuongXaId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("PhuongXaId");

                    b.ToTable("ThuongHieus");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.PhuongXa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<int>("QuanHuyenId")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<string>("TenDayDu")
                        .HasColumnType("longtext");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("QuanHuyenId");

                    b.ToTable("PhuongXas");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.QuanHuyen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<string>("TenDayDu")
                        .HasColumnType("longtext");

                    b.Property<int>("ThanhPhoId")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ThanhPhoId");

                    b.ToTable("QuanHuyens");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.ThanhPho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext");

                    b.Property<string>("Ten")
                        .HasColumnType("longtext");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("ThanhPhos");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.GotItTransactionRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Channel")
                        .HasColumnType("longtext");

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

                    b.Property<string>("ProductCode")
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

                    b.Property<int>("GotItTransactionRequestId")
                        .HasColumnType("int");

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

                    b.Property<string>("ProductCode")
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

                    b.Property<string>("Channel")
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

                    b.Property<string>("ProductCode")
                        .HasColumnType("longtext");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<string>("StateText")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .HasColumnType("longtext");

                    b.Property<string>("UsedBrand")
                        .HasColumnType("longtext");

                    b.Property<string>("UsedTime")
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

                    b.Property<string>("ProductCode")
                        .HasColumnType("longtext");

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

                    b.Property<string>("Channel")
                        .HasColumnType("longtext");

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

                    b.Property<string>("PropductCode")
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

                    b.Property<string>("ProductCode")
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

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Channel")
                        .HasColumnType("longtext");

                    b.Property<int?>("CityId")
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

                    b.Property<string>("Delivery")
                        .HasColumnType("longtext");

                    b.Property<string>("DeliveryCode")
                        .HasColumnType("longtext");

                    b.Property<string>("DeliveryNote")
                        .HasColumnType("longtext");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
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

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Pin")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductCode")
                        .HasColumnType("longtext");

                    b.Property<float>("ProductPrice")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UsedTime")
                        .HasColumnType("longtext");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("longtext");

                    b.Property<int?>("WardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UrboxTransactionResponses");
                });

            modelBuilder.Entity("CuaHangProduct", b =>
                {
                    b.Property<int>("CuaHangsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("CuaHangsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CuaHangProduct");
                });

            modelBuilder.Entity("CuaHangThuongHieu", b =>
                {
                    b.Property<int>("CuaHangsId")
                        .HasColumnType("int");

                    b.Property<int>("ThuongHieusId")
                        .HasColumnType("int");

                    b.HasKey("CuaHangsId", "ThuongHieusId");

                    b.HasIndex("ThuongHieusId");

                    b.ToTable("CuaHangThuongHieu");
                });

            modelBuilder.Entity("LinhVucThuongHieu", b =>
                {
                    b.Property<int>("LinhVucsId")
                        .HasColumnType("int");

                    b.Property<int>("ThuongHieusId")
                        .HasColumnType("int");

                    b.HasKey("LinhVucsId", "ThuongHieusId");

                    b.HasIndex("ThuongHieusId");

                    b.ToTable("LinhVucThuongHieu");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.CuaHang", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.PhuongXa", "PhuongXa")
                        .WithMany()
                        .HasForeignKey("PhuongXaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhuongXa");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.ThuongHieu", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.PhuongXa", "PhuongXa")
                        .WithMany()
                        .HasForeignKey("PhuongXaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhuongXa");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.PhuongXa", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.QuanHuyen", "QuanHuyen")
                        .WithMany("PhuongXas")
                        .HasForeignKey("QuanHuyenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuanHuyen");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.QuanHuyen", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.ThanhPho", "ThanhPho")
                        .WithMany("QuanHuyens")
                        .HasForeignKey("ThanhPhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThanhPho");
                });

            modelBuilder.Entity("CuaHangProduct", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.CuaHang", null)
                        .WithMany()
                        .HasForeignKey("CuaHangsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CuaHangThuongHieu", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.CuaHang", null)
                        .WithMany()
                        .HasForeignKey("CuaHangsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.ThuongHieu", null)
                        .WithMany()
                        .HasForeignKey("ThuongHieusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LinhVucThuongHieu", b =>
                {
                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.LinhVuc", null)
                        .WithMany()
                        .HasForeignKey("LinhVucsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs.ThuongHieu", null)
                        .WithMany()
                        .HasForeignKey("ThuongHieusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.QuanHuyen", b =>
                {
                    b.Navigation("PhuongXas");
                });

            modelBuilder.Entity("CoreLoyalty.F5Seconds.Domain.Entities.DiaChis.ThanhPho", b =>
                {
                    b.Navigation("QuanHuyens");
                });
#pragma warning restore 612, 618
        }
    }
}
