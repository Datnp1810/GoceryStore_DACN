using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.DTOs
{
    public class ThucDonNgayDTO
    {
            public int Ngay { get; set; } 
            public List<BuaAnReponse> Buas { get; set; }
    }
}
