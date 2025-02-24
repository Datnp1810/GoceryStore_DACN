using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.DTOs
{
    public class ThucPhamGroupDTO
    {
        public int ID_LoaiThucPham { get; set; }
        public string TenLoaiThucPham { get; set; }
        public List<ThucPhamGroupResponse> Products { get; set; }
    }
}
