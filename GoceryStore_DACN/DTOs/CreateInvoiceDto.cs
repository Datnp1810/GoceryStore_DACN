using GoceryStore_DACN.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.DTOs
{
    public class CreateInvoiceDto
    {
        public string UserId { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public double TongTien { get; set; } = 0;
        public string NoiNhan { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;
        //Quan hệ 1-n với tình trạng, là cha
        public int ID_TT { get; set; } = 1;
        //Quan hệ 1-n với Hinh thuc thanh toan, là cha
        public int ID_HinhThuc { get; set; } = 1; 
        public List<CreateInvoiceItemDto> InvoiceItems { get; set; } = new List<CreateInvoiceItemDto>();

    }
}
