namespace GoceryStore_DACN.DTOs
{
    public class InvoiceDetailDto
    {
        public int ThucPhamId { get; set; } = 0; 
        public string TenThucPham { get; set; } = string.Empty;
        public int SoLuong { get; set; } = 0;
        public int DonGia { get; set; } = 0;
        public int ThanhTien { get; set; } = 0;
    }
}
