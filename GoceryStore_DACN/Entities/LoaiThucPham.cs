using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Entities
{
    public class LoaiThucPham
    {
        [Key]
        public int ID_LoaiThucPham { get; set; }

        [Required]
        public string TenLoaiThucPham { get; set; }

        //Quan hệ n-n với thực phẩm

        public virtual ICollection<ThucPham>? ThucPham { get; set; }

    }
}
