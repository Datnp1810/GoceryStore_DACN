using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Entities
{
    public class CT_HoaDon
    {
        public int ID_HoaDon { get; set; }
        public int ID_ThucPham { get; set; }

        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }

        public ThucPham ThucPham { get; set; }
        public HoaDon HoaDon { get; set; }
  
        

    }
}
