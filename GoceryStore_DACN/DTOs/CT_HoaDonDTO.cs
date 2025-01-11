namespace GoceryStore_DACN.DTOs
{
    public class CT_HoaDonDTO
    {
        public int ID_HoaDon { get; set; }
        public int ID_ThucPham { get; set; }
        public string? TenThucPham { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
    }
}
