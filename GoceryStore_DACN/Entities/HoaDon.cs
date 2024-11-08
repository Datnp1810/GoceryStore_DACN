using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.Entities
{
    public class HoaDon
    {
        [Key]
        public int MAHD { get; set; }

        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
        public string NoiNhan { get; set; }
        public string GhiChu { get; set; }

        //Quan hệ 1-n với tình trạng, là cha
        [ForeignKey("TinhTrang")] public int ID_TT { get; set; }
        public virtual TinhTrang? TinhTrang { get; set; }

        //Quan hệ 1-n với Hinh thuc thanh toan, là cha
        [ForeignKey("HinhThucThanhToan")] public int ID_HinhThuc { get; set; }
        public virtual HinhThucThanhToan? HinhThucThanhToan { get; set; }

        // Quan hệ một-nhiều với CT_HoaDon
        public ICollection<CT_HoaDon>? CTHoaDons { get; set; }

        public HoaDon()
        {
            CTHoaDons = new List<CT_HoaDon>();
        }
    }
}
