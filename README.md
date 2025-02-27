# GoceryStore_DACN

**_GoceryStore_DACN_** là một ứng dụng bán các mặt hàng Thực Phẩm. Có tích hợp gợi ý thực đơn theo ngày cho người dùng và gọi ý thực đơn cho người dùng theo **Chế Độ Ăn**(Bình Thường, Tăng Cân, Giảm Cân) dành cho đối tượng Nam và Nữ ở độ tuổi trưởng thành.

## Mục tiêu dự án

Dự án này hướng tới việc xây dựng một hệ thống quản lý thực phẩm, thành phần dinh dưỡng và thực đơn cho cửa hàng bán lẻ. Hệ thống sẽ cung cấp một nền tảng dễ sử dụng để quản lý dữ liệu sản phẩm, đồng thời hỗ trợ các cửa hàng trong việc xây dựng thực đơn dinh dưỡng cho khách hàng.

## Các Tính Năng Chính

- **Quản lý sản phẩm Thực Phẩm**: Lưu trữ thông tin chi tiết về thực phẩm, các món ăn, thành phần dinh dưỡng, và giá trị dinh dưỡng.
- **Thực đơn dinh dưỡng**: Xây dựng và quản lý thực đơn cho cửa hàng, giúp tạo các bữa ăn bổ dưỡng cho khách hàng.
- **API truy xuất thông tin**: Cung cấp API để truy xuất thông tin về sản phẩm, món ăn và thành phần dinh dưỡng.
- **Gợi ý thực phẩm dựa trên thực đơn đã gợi ý**: Sau khi có thực đơn gợi ý, ứng dụng sẽ giúp người dùng soạn sẵn các thực phẩm cần mua sao cho khớp với thực đơn.

## Cài Đặt và Sử Dụng

### Clone Repository

##### 1.Back-End

Để bắt đầu, bạn cần clone kho lưu trữ này về máy của mình. Mở terminal và chạy lệnh sau:

```bash
git clone https://github.com/Datnp1810/GoceryStore_DACN.git
```

Sau đó vào thư mục để mở dự án lên với phần mềm Visual Studio 2022

##### 2.Font-End

Sau khi hoàn tất việc cài đặt Back-End về máy, tiếp tục clone Font-End về. Mở terminal và chạy lệnh sau:

```bash
git clone https://github.com/Datnp1810/GoceryStore_Template.git
```

Tiếp theo mở dự án với phần mềm Visual Studio Code

### Hoàn Tất Cài Đặt

Sau khi hoàn tất cài đặt Font-End và Back-End bạn hãy chạy song song Font-End và Back-End với nhau

## Công Nghệ Sử Dụng

- **.NET Core (C#)**: Framework chính cho ứng dụng.
- **Entity Framework Core**: ORM (Object-Relational Mapping) cho phép truy cập và thao tác với cơ sở dữ liệu.
- **SQL Server**: Hệ quản trị cơ sở dữ liệu được sử dụng trong dự án.
- **ASP.NET Core**: Được sử dụng để xây dựng các API và giao diện web.
- **Swagger**: Dùng để tạo và kiểm tra các API dễ dàng
- **Thuật toán GA**: Sử dụng để xây dựng chức năng gợi ý thực đơn theo Chế Độ Ăn của người dùng
- **Phân luồng, Memory Cache**: Tối ưu hóa thuật toán
## Link trang web sau khi deploy
[https://gocery-store.vercel.app/index.html](https://gocery-store.vercel.app/index.html)

## Cách Đóng Góp

1. Fork kho lưu trữ này.
2. Tạo một nhánh mới từ nhánh chính (git checkout -b feature/feature-name).
3. Thực hiện các thay đổi trong nhánh của bạn.
4. Đẩy nhánh của bạn lên GitHub (git push origin feature/feature-name).
5. Mở một Pull Request vào nhánh chính của kho lưu trữ để chúng tôi xem xét và hợp nhất vào mã nguồn chính.

## Liên Hệ

Email: [nguyendangne@gmail.com]()
GitHub: [@Datnp1810](https://github.com/Datnp1810)
