using GoceryStore_DACN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GoceryStore_DACN.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Cấu hình 1-1 cho bảng Thực phẩm - thành phần dinh dưỡng
            modelBuilder.Entity<ThucPham>()
                .HasOne(tp => tp.ThanhPhanDinhDuong)
                .WithOne(td => td.ThucPham)
                .HasForeignKey<ThanhPhanDinhDuong>(td => td.ID_ThucPham)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CT_HoaDon>(e =>
            {
                e.ToTable("CT_HoaDon");
                e.HasKey(e => new { e.ID_HoaDon, e.ID_ThucPham});

                e.HasOne(s => s.HoaDon)
                    .WithMany(s => s.CTHoaDons)
                    .HasForeignKey(s => s.ID_HoaDon);

                e.HasOne(s => s.ThucPham)
                    .WithMany(s => s.CT_HoaDons)
                    .HasForeignKey(s => s.ID_ThucPham);
            });

            modelBuilder.Entity<CT_BuoiAn>(e =>
            {
                e.ToTable("CT_BuoiAn");
                e.HasKey(e => new { e.ID_MonAn, e.ID_ThucPham });

                e.HasOne(s => s.MonAn)
                    .WithMany(s => s.CTBuoiAns)
                    .HasForeignKey(s => s.ID_MonAn);

                e.HasOne(s => s.ThucPham)
                    .WithMany(s => s.CT_BuoiAns)
                    .HasForeignKey(s => s.ID_ThucPham);
            });

            var tinhTrangDons = new List<TinhTrang>
            {
                new()
                {
                    ID_TT = 1,
                    TenTinhTrang = "Chờ xác nhận"
                },
                new()
                {
                    ID_TT = 2,
                    TenTinhTrang = "Đang giao"
                },
                new()
                {
                    ID_TT = 3,
                    TenTinhTrang = "Đã giao"
                },
                new()
                {
                    ID_TT = 4,
                    TenTinhTrang = "Đã hủy"
                }
            };
            modelBuilder.Entity<TinhTrang>().HasData(tinhTrangDons);


        }
        //DB Set
        #region DbSet

        public DbSet<LoaiThucPham> LoaiThucPhams { get; set; }
        public DbSet<ThucPham> ThucPhams { get; set; }
        public DbSet<ThanhPhanDinhDuong> ThanhPhanDinhDuongs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<CT_HoaDon> CTHoaDons { get; set; }
        public DbSet<TinhTrang> TinhTrangs { get; set; }
        public DbSet<HinhThucThanhToan> HinhThucThanhToans{ get; set; }
        public DbSet<CheDoAn> CheDoAns { get; set; }
        public DbSet<CT_BuoiAn> CTBuoiAns { get; set; }
        public DbSet<MonAn> MonAns { get; set; }
        public DbSet<LoaiMonAn> LoaiMonAns { get; set; }
        #endregion

    }

}
