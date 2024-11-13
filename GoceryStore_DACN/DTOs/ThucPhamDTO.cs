using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.DTOs
{
    public class ThucPhamDTO
    {
        [Required]
        public string TenThucPham { get; set; } = string.Empty;
        [Required]
        public int ID_LoaiThucPham { get; set; }
        public string DVT { get; set; } = "Kg";
        public int SoLuong { get; set; } = 0;
        public double GiaBan { get; set; } = 0;
        public IFormFile ImageFile { get; set; } = null;
        public string TrangThai { get; set; } = "Còn Hàng";
    }
}
