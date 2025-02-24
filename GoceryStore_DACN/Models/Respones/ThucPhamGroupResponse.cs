namespace GoceryStore_DACN.Models.Respones
{
    public class ThucPhamGroupResponse
    {
        public int ID_ThucPham { get; set; }
        public string TenThucPham { get; set; }
        public double GiaBan { get; set; }
        public double? SoLuong { get; set; }
        public string Image { get; set; }
    }
}
