namespace GoceryStore_DACN.Models.Respones
{
    public class BuaAnReponse
    {
        public string Buoi { get; set; }
        public Dictionary<int, MonAnResponse> MonAn { get; set; }
    }
}
