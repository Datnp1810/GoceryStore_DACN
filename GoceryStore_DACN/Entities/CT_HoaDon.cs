using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.Entities
{
    public class CT_HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCT_HoaDon { get; set; }
        [ForeignKey("HoaDon")]
        public int ID_HoaDon { get; set; }
        [ForeignKey("ThucPham")]
        public int ID_ThucPham { get; set; }

        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien { get; set; }
        public ThucPham ThucPham { get; set; }
        public HoaDon HoaDon { get; set; }
    }
}
