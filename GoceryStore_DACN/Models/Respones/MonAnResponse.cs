namespace GoceryStore_DACN.Models.Respones
{
    public class MonAnResponse
    {
        public int ID_TenMonAn {  get; set; }
        public string TenMonAn { get; set; }

        //Quan hệ 1-nhiều với Loại Món Ăn
        public int ID_LoaiMonAn { get; set; }
        public string TenLoaiMonAn { get; set; }
    }
}
