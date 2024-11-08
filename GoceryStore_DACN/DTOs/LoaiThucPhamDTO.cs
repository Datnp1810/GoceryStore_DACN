using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.DTOs
{
    public class LoaiThucPhamDTO
    {
        [Required]
        public string TenLoaiThucPham { get; set; }
    }
}
