using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.DTOs
{
    public class ThanhPhanDinhDuongDTO
    {
        [Key, ForeignKey("ThucPham")]
        public int ID_ThucPham { get; set; }

        public float? Nuoc { get; set; }
        public float? Energy { get; set; }
        public float? Protein { get; set; }
        public float? Fat { get; set; }
        public float? Carbohydrate { get; set; }
        public float? ChatXo { get; set; }
        public float? Canxi { get; set; }
        public float? Fe { get; set; }
        public float? VitaminC { get; set; }
        public float? VitaminB1 { get; set; }
        public float? VitaminA { get; set; }
        public float? Photpho { get; set; }
        public float? Kali { get; set; }
        public float? Magie { get; set; }
    }
}
