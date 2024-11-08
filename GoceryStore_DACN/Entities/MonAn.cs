using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.Entities
{
    public class MonAn
    {
        [Key]
        public int ID_MonAn { get; set; }

        [Required]
        public string TenMonAn { get; set; }

        //Quan hệ 1-nhiều với Loại Món Ăn
        [ForeignKey("LoaiMonAn")] public int ID_LoaiMonAn { get; set; }
        public virtual LoaiMonAn? LoaiMonAn { get; set; }

        // Quan hệ một-nhiều với CT_BuoiAn
        public ICollection<CT_BuoiAn>? CTBuoiAns { get; set; }

        public MonAn()
        {
            CTBuoiAns = new HashSet<CT_BuoiAn>();
        }
    }
}
