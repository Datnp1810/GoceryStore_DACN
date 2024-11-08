using GoceryStore_DACN.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.DTOs
{
    public class HoaDonDTO
    {
        public DateTime NgayLap { get; set; }
        public double TongTien { get; set; }
        public string NoiNhan { get; set; }
        public string GhiChu { get; set; }

        //Quan hệ 1-n với tình trạng, là cha
        public int ID_TT { get; set; }

        //Quan hệ 1-n với Hinh thuc thanh toan, là cha
        public int ID_HinhThuc { get; set; }

    }
}
