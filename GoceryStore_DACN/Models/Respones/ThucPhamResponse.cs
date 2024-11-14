using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Models.Respones
{
    public class ThucPhamResponse
    {
        public int ID_ThucPham { get; set; }
        public string TenThucPham { get; set; }
        public string DVT { get; set; } = string.Empty;
        public int? SoLuong { get; set; } = 0;
        public double GiaBan { get; set; }
        public string Image { get; set; }
        public string TrangThai { get; set; }
        public int ID_LoaiThucPham { get; set; }
        public string TenLoaiThucPham { get; set; }


        
    }

}
