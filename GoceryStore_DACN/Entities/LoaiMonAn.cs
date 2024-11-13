using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Entities
{
    public class LoaiMonAn
    {
        [Key]
        public int ID_LoaiMonAn { get; set; }

        [Required]
        public string TenLoaiMonAn { get; set; }

        // Quan hệ một-nhiều với MonAn
        public ICollection<MonAn>? MonAns { get; set; }
    }
}
