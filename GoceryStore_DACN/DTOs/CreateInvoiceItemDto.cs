namespace GoceryStore_DACN.DTOs
{
    public class CreateInvoiceItemDto
    {
        public int ThucPhamId { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double TongTien { get; set; }
        public CreateInvoiceItemDto() { }

    }
}
