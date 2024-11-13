﻿// <auto-generated />
using System;
using GoceryStore_DACN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoceryStore_DACN.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoceryStore_DACN.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CT_BuoiAn", b =>
                {
                    b.Property<int>("ID_MonAn")
                        .HasColumnType("int");

                    b.Property<int>("ID_ThucPham")
                        .HasColumnType("int");

                    b.Property<int>("ID_CDA")
                        .HasColumnType("int");

                    b.HasKey("ID_MonAn", "ID_ThucPham");

                    b.HasIndex("ID_CDA");

                    b.HasIndex("ID_ThucPham");

                    b.ToTable("CT_BuoiAn", (string)null);
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CT_HoaDon", b =>
                {
                    b.Property<int>("ID_HoaDon")
                        .HasColumnType("int");

                    b.Property<int>("ID_ThucPham")
                        .HasColumnType("int");

                    b.Property<double>("DonGia")
                        .HasColumnType("float");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("ID_HoaDon", "ID_ThucPham");

                    b.HasIndex("ID_ThucPham");

                    b.ToTable("CT_HoaDon", (string)null);
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CheDoAn", b =>
                {
                    b.Property<int>("ID_CDA")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_CDA"));

                    b.Property<string>("TenCheDoAn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_CDA");

                    b.ToTable("CheDoAns");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.HinhThucThanhToan", b =>
                {
                    b.Property<int>("ID_HinhThuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_HinhThuc"));

                    b.Property<string>("HTThanhToan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_HinhThuc");

                    b.ToTable("HinhThucThanhToans");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.HoaDon", b =>
                {
                    b.Property<int>("MAHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MAHD"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_HinhThuc")
                        .HasColumnType("int");

                    b.Property<int>("ID_TT")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MAHD");

                    b.HasIndex("ID_HinhThuc");

                    b.HasIndex("ID_TT");

                    b.HasIndex("UserId");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.LoaiMonAn", b =>
                {
                    b.Property<int>("ID_LoaiMonAn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_LoaiMonAn"));

                    b.Property<string>("TenLoaiMonAn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_LoaiMonAn");

                    b.ToTable("LoaiMonAns");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.LoaiThucPham", b =>
                {
                    b.Property<int>("ID_LoaiThucPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_LoaiThucPham"));

                    b.Property<string>("TenLoaiThucPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_LoaiThucPham");

                    b.ToTable("LoaiThucPhams");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.MonAn", b =>
                {
                    b.Property<int>("ID_MonAn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_MonAn"));

                    b.Property<int>("ID_LoaiMonAn")
                        .HasColumnType("int");

                    b.Property<string>("TenMonAn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_MonAn");

                    b.HasIndex("ID_LoaiMonAn");

                    b.ToTable("MonAns");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.ThanhPhanDinhDuong", b =>
                {
                    b.Property<int>("ID_ThucPham")
                        .HasColumnType("int");

                    b.Property<float?>("Canxi")
                        .HasColumnType("real");

                    b.Property<float?>("Carbohydrate")
                        .HasColumnType("real");

                    b.Property<float?>("ChatXo")
                        .HasColumnType("real");

                    b.Property<float?>("Energy")
                        .HasColumnType("real");

                    b.Property<float?>("Fat")
                        .HasColumnType("real");

                    b.Property<float?>("Fe")
                        .HasColumnType("real");

                    b.Property<float?>("Kali")
                        .HasColumnType("real");

                    b.Property<float?>("Magie")
                        .HasColumnType("real");

                    b.Property<float?>("Nuoc")
                        .HasColumnType("real");

                    b.Property<float?>("Photpho")
                        .HasColumnType("real");

                    b.Property<float?>("Protein")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminA")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB1")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminB2")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminC")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminD")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminE")
                        .HasColumnType("real");

                    b.Property<float?>("VitaminK")
                        .HasColumnType("real");

                    b.HasKey("ID_ThucPham");

                    b.ToTable("ThanhPhanDinhDuongs");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.ThucPham", b =>
                {
                    b.Property<int>("ID_ThucPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_ThucPham"));

                    b.Property<string>("DVT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GiaBan")
                        .HasColumnType("float");

                    b.Property<int>("ID_LoaiThucPham")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenThucPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_ThucPham");

                    b.HasIndex("ID_LoaiThucPham");

                    b.ToTable("ThucPhams");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.TinhTrang", b =>
                {
                    b.Property<int>("ID_TT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_TT"));

                    b.Property<string>("TenTinhTrang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_TT");

                    b.ToTable("TinhTrangs");

                    b.HasData(
                        new
                        {
                            ID_TT = 1,
                            TenTinhTrang = "Chờ xác nhận"
                        },
                        new
                        {
                            ID_TT = 2,
                            TenTinhTrang = "Đang giao"
                        },
                        new
                        {
                            ID_TT = 3,
                            TenTinhTrang = "Đã giao"
                        },
                        new
                        {
                            ID_TT = 4,
                            TenTinhTrang = "Đã hủy"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CT_BuoiAn", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.CheDoAn", "CheDoAn")
                        .WithMany("CTBuoiAn")
                        .HasForeignKey("ID_CDA")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.MonAn", "MonAn")
                        .WithMany("CTBuoiAns")
                        .HasForeignKey("ID_MonAn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.ThucPham", "ThucPham")
                        .WithMany("CT_BuoiAns")
                        .HasForeignKey("ID_ThucPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheDoAn");

                    b.Navigation("MonAn");

                    b.Navigation("ThucPham");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CT_HoaDon", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.HoaDon", "HoaDon")
                        .WithMany("CTHoaDons")
                        .HasForeignKey("ID_HoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.ThucPham", "ThucPham")
                        .WithMany("CT_HoaDons")
                        .HasForeignKey("ID_ThucPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("ThucPham");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.HoaDon", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.HinhThucThanhToan", "HinhThucThanhToan")
                        .WithMany("HoaDon")
                        .HasForeignKey("ID_HinhThuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.TinhTrang", "TinhTrang")
                        .WithMany("HoaDon")
                        .HasForeignKey("ID_TT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("HinhThucThanhToan");

                    b.Navigation("TinhTrang");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.MonAn", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.LoaiMonAn", "LoaiMonAn")
                        .WithMany("MonAns")
                        .HasForeignKey("ID_LoaiMonAn")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiMonAn");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.ThanhPhanDinhDuong", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.ThucPham", "ThucPham")
                        .WithOne("ThanhPhanDinhDuong")
                        .HasForeignKey("GoceryStore_DACN.Entities.ThanhPhanDinhDuong", "ID_ThucPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThucPham");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.ThucPham", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.LoaiThucPham", "LoaiThucPham")
                        .WithMany("ThucPham")
                        .HasForeignKey("ID_LoaiThucPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiThucPham");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoceryStore_DACN.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GoceryStore_DACN.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.CheDoAn", b =>
                {
                    b.Navigation("CTBuoiAn");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.HinhThucThanhToan", b =>
                {
                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.HoaDon", b =>
                {
                    b.Navigation("CTHoaDons");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.LoaiMonAn", b =>
                {
                    b.Navigation("MonAns");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.LoaiThucPham", b =>
                {
                    b.Navigation("ThucPham");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.MonAn", b =>
                {
                    b.Navigation("CTBuoiAns");
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.ThucPham", b =>
                {
                    b.Navigation("CT_BuoiAns");

                    b.Navigation("CT_HoaDons");

                    b.Navigation("ThanhPhanDinhDuong")
                        .IsRequired();
                });

            modelBuilder.Entity("GoceryStore_DACN.Entities.TinhTrang", b =>
                {
                    b.Navigation("HoaDon");
                });
#pragma warning restore 612, 618
        }
    }
}
