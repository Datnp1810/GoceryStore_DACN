using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.DTOs
{
    public class CT_BuoiAnDTO
    {
        public int ID_ThucPham { get; set; }
        public int ID_MonAn { get; set; }
        public double Gram { get; set; }
        
    }
}
