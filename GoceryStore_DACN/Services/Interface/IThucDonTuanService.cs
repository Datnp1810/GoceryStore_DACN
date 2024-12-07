using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IThucDonTuanService
    {
        public Task<double> Fitness(ThucDonTuanResponse thucDonTuan, int maCheDoAn);
        public  Task<ThucDonTuanResponse> ThuatToanGA(int maCheDoAn);
        public ThucDonTuanResponse GenerateThucDonTuan();
        public List<ThucDonTuanResponse> KhoiTaoQuanThe();
        public ThucDonNgayResponse GenerateThucDonNgay(int ngay, Dictionary<int, Queue<int>> history);
    }
}
