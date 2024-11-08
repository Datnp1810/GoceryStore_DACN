using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.Entities
{
    public class ThucPham
    {
        [Key]
        public int ID_ThucPham { get; set; }


        [Required]
        public string TenThucPham { get; set; }

        public string DVT { get; set; }
        public int? SoLuong { get; set; }
        public double GiaBan { get; set; }
        public string Image { get; set; }
        public string TrangThai { get; set; }

        // Quan hệ một-một với ThanhPhanDinhDuong
        public virtual ThanhPhanDinhDuong ThanhPhanDinhDuong { get; set; }

        // Quan hệ một-nhiều với CT_HoaDon
        public ICollection<CT_HoaDon>? CT_HoaDons { get; set; }

        //Quan hệ 1 -n với CT_BuoiAn
        public ICollection<CT_BuoiAn>? CT_BuoiAns { get; set; }

        public ThucPham()
        {
            CT_HoaDons = new HashSet<CT_HoaDon>();
            CT_BuoiAns = new List<CT_BuoiAn>();
        }


        // Quan hệ một-nhiều với LoaiThucPham
        [ForeignKey("LoaiThucPham")] public int ID_LoaiThucPham { get; set; }
        public virtual LoaiThucPham? LoaiThucPham { get; set; }

    }
}
