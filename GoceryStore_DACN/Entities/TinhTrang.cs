using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Entities
{
    public class TinhTrang
    {
        [Key]
        public int ID_TT { get; set; }
        [Required]
        public string TenTinhTrang { get; set; }

        public ICollection<HoaDon>? HoaDon { get; set; }
    }
}
