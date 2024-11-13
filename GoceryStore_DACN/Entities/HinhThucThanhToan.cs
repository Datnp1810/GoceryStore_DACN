using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace GoceryStore_DACN.Entities
{
    public class HinhThucThanhToan
    {
        [Key]
        public int ID_HinhThuc { get; set; }

        [Required]
        public string HTThanhToan { get; set; }

        public ICollection<HoaDon>? HoaDon { get; set; }
    }
}
