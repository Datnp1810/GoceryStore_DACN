using System.Reflection.Metadata;

namespace GoceryStore_DACN.DTOs
{
    public class HoaDonDTO
    {
        //public int MaHoaDon { get; set; } = 0;
        public int IdTinhTrang { get; set; } = 1;
        public int IdHinhThucThanhToan { get; set; } = 1;
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; } = DateTime.Now; 
        public double TongTien { get; set; } = 0;
        public string NoiNhan { get; set; } = string.Empty;
        public string? GhiChu { get; set; } = string.Empty;

    }
}
