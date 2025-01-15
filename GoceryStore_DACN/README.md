  # 🛒 Grocery Store API

  ## 📝 Giới thiệu
  Đây là API backend cho ứng dụng Grocery Store, được xây dựng bằng ASP.NET Core. API này cung cấp các chức năng quản lý cửa hàng tạp hóa toàn diện, bao gồm quản lý người dùng, sản phẩm, đơn hàng và thực đơn dinh dưỡng.

  ## 🛠️ Công nghệ sử dụng
  - ASP.NET Core 6.0
  - Entity Framework Core
  - SQL Server
  - JWT Authentication
  - Identity Framework
  - AutoMapper
  - Cloudinary (cho lưu trữ hình ảnh)

  ## ✨ Tính năng chính

  ### 👥 1. Quản lý người dùng
  - Đăng ký tài khoản mới
  - Xác thực email người dùng
  - Đăng nhập/Đăng xuất an toàn
  - Quản lý thông tin cá nhân
  - Phân quyền người dùng (Admin/User)
  - Quên mật khẩu và đặt lại mật khẩu

  ### 📦 2. Quản lý sản phẩm
  - CRUD thực phẩm đầy đủ
  - Quản lý danh mục sản phẩm
  - Upload và quản lý hình ảnh sản phẩm
  - Quản lý giá cả và theo dõi tồn kho

  ### 🛍️ 3. Quản lý đơn hàng
  - Tạo đơn hàng mới
  - Quản lý giỏ hàng thông minh
  - Theo dõi trạng thái đơn hàng realtime
  - Xử lý thanh toán đa dạng
  - Lịch sử đơn hàng chi tiết

  ### 🍽️ 4. Thực đơn và dinh dưỡng
  - Quản lý thực đơn linh hoạt
  - Thông tin dinh dưỡng chi tiết
  - Gợi ý thực đơn theo tuần thông minh
  - Tính toán khẩu phần ăn khoa học

  ## ⚙️ Cài đặt và Chạy

  ### Yêu cầu hệ thống
  - .NET 6.0 SDK
  - SQL Server
  - Visual Studio 2022 hoặc VS Code

  ### Các bước cài đặt

  1. Clone repository:
  ```bash
  git clone [repository-url]
  ```
  2. Cấu hình connection string trong appsettings.json:
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=your_server;Database=your_database;Trusted_Connection=True;"
    }
  }
  ```
  3. Chạy migration:
  ```bash
  dotnet ef database update
  ```
  4. Khởi chạy project:
  ```bash
  dotnet run
  ```

  ## 📁 Cấu trúc project
  ```
  GroceryStore/
  ├── Controllers/    # API Controllers
  ├── Services/       # Business Logic
  ├── Repositories/   # Data Access Layer
  ├── Models/         # Data Models
  ├── DTOs/          # Data Transfer Objects
  ├── Entities/      # Database Entities
  ├── Helpers/       # Utility Classes
  └── Middlewares/   # Custom Middlewares
  ```

  ## 🔌 API Endpoints

  ### 🔐 Authentication & User Management
  - POST /api/auth/register - Đăng ký tài khoản mới
  - POST /api/auth/login - Đăng nhập hệ thống
  - POST /api/auth/forgot-password - Yêu cầu khôi phục mật khẩu
  - POST /api/auth/reset-password - Đặt lại mật khẩu
  - POST /api/auth/confirm-email - Xác nhận email
  - POST /api/auth/resend-confirmation - Gửi lại email xác nhận

  ### 👤 Users
  - GET /api/users/profile - Lấy thông tin người dùng
  - PUT /api/users/update-user-info - Cập nhật thông tin cá nhân
  - POST /api/users/change-password - Thay đổi mật khẩu
  - DELETE /api/users/delete-account - Xóa tài khoản

  ### 👨‍💼 Admin Management
  - GET /api/admin/users - Lấy danh sách người dùng
  - GET /api/admin/users/{userId} - Xem chi tiết người dùng
  - DELETE /api/admin/users/{userId} - Xóa người dùng
  - POST /api/admin/users/{userId}/roles - Thêm role cho người dùng
  - DELETE /api/admin/users/{userId}/roles/{roleName} - Xóa role của người dùng
  - GET /api/admin/orders - Xem danh sách đơn hàng
  - PUT /api/admin/orders/{orderId}/status - Cập nhật trạng thái đơn hàng

  ### 🏷️ Products
  - GET /api/products - Lấy danh sách sản phẩm
  - GET /api/products/{id} - Xem chi tiết sản phẩm
  - POST /api/products - Thêm sản phẩm mới
  - PUT /api/products/{id} - Cập nhật thông tin sản phẩm
  - DELETE /api/products/{id} - Xóa sản phẩm
  - POST /api/products/upload-image - Upload hình ảnh sản phẩm

  ### 🛒 Shopping Cart & Orders
  - GET /api/orders - Lấy danh sách đơn hàng
  - GET /api/orders/{id} - Xem chi tiết đơn hàng
  - POST /api/orders - Tạo đơn hàng mới
  - PUT /api/orders/{id} - Cập nhật đơn hàng
  - GET /api/cart - Xem giỏ hàng
  - PUT /api/cart/update-quantity - Cập nhật số lượng trong giỏ hàng
  - POST /api/cart/checkout - Thanh toán giỏ hàng

  ### 🍽️ Nutrition & Menu
  - GET /api/nutrition/{productId} - Xem thông tin dinh dưỡng
  - GET /api/menu/suggestions - Gợi ý thực đơn
  - GET /api/menu/weekly - Xem thực đơn theo tuần
  - POST /api/menu/calculate-portion - Tính toán khẩu phần ăn

  ## 🔒 Bảo mật
  - JWT Authentication
  - Role-based Authorization 
  - Email Verification
  - Password Hashing
  - HTTPS

  ## 👥 Người đóng góp
  - [Tên của bạn]
  - [Tên thành viên khác]

  ## 📄 License
  Không có giấy phép
