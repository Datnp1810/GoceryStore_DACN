using GoceryStore_DACN.Entities;

namespace GoceryStore_DACN.Models.Respones
{
    public class CT_BuoiAnResponse
    {
        public int ID_ThucPham { get; set; }
        public int ID_MonAn { get; set; }
        public double? Gram { get; set; }
        // Quan hệ một-nhiều với Chế Độ Ăn
    }
}
