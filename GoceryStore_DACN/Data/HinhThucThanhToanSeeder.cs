using GoceryStore_DACN.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoceryStore_DACN.Data
{
    public static class HinhThucThanhToanSeeder
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
           context.Database.Migrate();
            if (!context.HinhThucThanhToans.Any())
            {
                context.HinhThucThanhToans.AddRange(new List<HinhThucThanhToan>
                {
                    new()
                    {
                        HTThanhToan = "Thanh toán khi nhận hàng"
                    },
                    new()
                    {
                        HTThanhToan = "Thanh toán qua thẻ"
                    },
                    new()
                    {
                        HTThanhToan = "Thanh toán qua ví điện tử"
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
