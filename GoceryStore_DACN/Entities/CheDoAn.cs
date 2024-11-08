using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Entities
{
    public class CheDoAn
    {
        [Key]
        public int ID_CDA { get; set; }

        [Required]
        public string TenCheDoAn { get; set; }

        // Quan hệ một-nhiều với CT_BuoiAn
        public ICollection<CT_BuoiAn>? CTBuoiAn { get; set; }

        public CheDoAn()
        {
            CTBuoiAn = new HashSet<CT_BuoiAn>();
        }

    }
}
