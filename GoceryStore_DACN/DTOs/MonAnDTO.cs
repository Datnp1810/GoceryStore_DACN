using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.DTOs
{
    public class MonAnDTO
    {
        [Required]
        public string TenMonAn { get; set; }

        //Quan hệ 1-nhiều với Loại Món Ăn
        public int ID_LoaiMonAn { get; set; }
    }
}
