using GoceryStore_DACN.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.DTOs
{
    public class CreateHoaDonDto
    { 
        public string NoiNhan { get; set; } = string.Empty;
        public string GhiChu { get; set; } = string.Empty;
        public int ID_HinhThuc { get; set; } = 1; 
        public List<CreateInvoiceItemDto> InvoiceItems { get; set; }

    }
}
