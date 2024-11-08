using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.DTOs
{
    public class ThucPhamDTO
    {
        [Required]
        public string TenThucPham { get; set; }
        public int ID_LoaiThucPham { get; set; }
        public string DVT { get; set; }
        public int? SoLuong { get; set; }
        public double GiaBan { get; set; }
        public string Image { get; set; }
        public string TrangThai { get; set; }
    }
}
