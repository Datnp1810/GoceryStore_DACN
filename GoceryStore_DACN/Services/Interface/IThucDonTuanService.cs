using GoceryStore_DACN.Models.Respones;

namespace GoceryStore_DACN.Services.Interface
{
    public interface IThucDonTuanService
    {
        public Task<double> Fitness(ThucDonTuanResponse thucDonTuan, int maCheDoAn);
        public  Task<ThucDonTuanResponse> ThuatToanGA(int maCheDoAn);
        public Task<ThucDonTuanResponse> GenerateThucDonTuan();
        public Task<ThucDonNgayResponse> GenerateThucDonNgay(int ngay, Dictionary<string, Queue<int>> history);
    }
}
