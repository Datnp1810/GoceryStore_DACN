namespace GoceryStore_DACN.DTOs
{
    public class InvoiceDto
    {
        public int MaHoaDon { get; set; } = 0;
        public int IdTinhTrang { get; set; } = 0;
        public int IdHinhThucThanhToan { get; set; } = 0;
        public string UserId { get; set; } = string.Empty;
        public DateTime NgayLap { get; set; } = DateTime.Now; 
        public double TongTien { get; set; } = 0;
        public string NoiNhan { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;
        public List<InvoiceDetailDto> ChiTietHoaDons { get; set; }

    }
}
