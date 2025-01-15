using GoceryStore_DACN.Entities;
using Microsoft.AspNetCore.Identity;
namespace GoceryStore_DACN.Data.Seeders
{
  public static class AdminAccountSeeder
  {
    public static async Task SeedAdminAccount(IServiceProvider serviceProvider)
    {
      using var scope = serviceProvider.CreateScope();
      var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

      // Kiểm tra và tạo admin role nếu chưa tồn tại
      if (!await roleManager.RoleExistsAsync("Admin"))
      {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
      }

      // Thông tin tài khoản admin
      var adminEmail = "admin@gmail.com";
      var adminPassword = "Admin@123"; // Nên đặt trong configuration

      // Kiểm tra nếu admin chưa tồn tại thì tạo mới
      var adminUser = await userManager.FindByEmailAsync(adminEmail);
      if (adminUser == null)
      {
        adminUser = new ApplicationUser
        {
          UserName = "admin",
          Email = adminEmail,
          EmailConfirmed = true, // Email đã được xác nhận
          PhoneNumber = "0123456789",
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(adminUser, "Admin");
        }
      }
    }
  }
}