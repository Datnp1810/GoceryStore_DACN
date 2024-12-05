using GoceryStore_DACN.Data;
using GoceryStore_DACN.Entities;
using GoceryStore_DACN.Models.Respones;
using GoceryStore_DACN.Repositories.Interface;
using GoceryStore_DACN.Services.Interface;
using GroceryStore_DACN.Repositories.Interface;
using System.Linq;
namespace GoceryStore_DACN.Services
{
    public class ThucDonTuanService : IThucDonTuanService
    {
        private readonly IMonAnRepository _repository;
        private readonly IChiTietBuoiAnRepository _ct_BuoiAnRepository;
        private readonly ICheDoAnRepository _cheDoAnRepository;
        private readonly IThanhPhanDinhDuongRepository _thanhPhanDinhDuongRepository;
        private readonly ApplicationDbContext _dbContext;



        const int SoLuongQuanThe = 100;
        const int SoLanChay= 20;
        const int SoLuongQuanTheChon = 10;


        public ThucDonTuanService(IMonAnRepository repository, 
            IChiTietBuoiAnRepository chiTietBuoiAnRepository, 
            IThanhPhanDinhDuongRepository _repo,
            ICheDoAnRepository cheDoAnRepository,
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repository = repository;
            _ct_BuoiAnRepository = chiTietBuoiAnRepository;
             _thanhPhanDinhDuongRepository = _repo;
            _cheDoAnRepository = cheDoAnRepository;
        }
        #region Generate Cá Thể, Gen(Phục vụ cho đột biến)
        public async Task<ThucDonNgayResponse> GenerateThucDonNgay(int ngay, Dictionary<string, Queue<int>> history)
        {

            if (history.Count == 0)
            {
                history = new Dictionary<string, Queue<int>>();
                foreach (var loai in new[] { "Canh", "Xào", "Mặn" })
                {
                    history[loai] = new Queue<int>();
                }
            }
            var thucDonNgay = new ThucDonNgayResponse
            {
                Ngay = ngay,
                Buas = new List<BuaAnReponse>(),
                TongNangLuong = 0,
                TongProtein = 0,
                TongChatBeo = 0,
                TongCarbohydrate = 0,
                TongChatXo = 0,
                TongCanxi = 0,
                TongSat = 0,
                TongMagie = 0,
                TongPhotpho = 0,
                TongKali = 0,
                TongVitaminC = 0,
                TongVitaminB1 = 0,
                TongVitaminB2 = 0,
                TongVitaminA = 0,
                TongVitaminD = 0,
                TongVitaminE = 0,
                TongVitaminK = 0
            };

            // Tạo thực đơn cho 3 bữa mỗi ngày
            for (int bua = 1; bua <= 3; bua++)
            {
                var buaAn = new BuaAnReponse
                {
                    Buoi = bua == 1 ? "Sáng" : bua == 2 ? "Trưa" : "Tối",
                    MonAn = new Dictionary<string, MonAnResponse>()
                };

                // Chọn món cho từng loại món
                foreach (var loai in new[] { "Cơm", "Canh", "Xào", "Mặn" })
                {
                    var danhSachMon = await _repository.GetAllMonAnByLoaiMonAn(loai);
                    MonAnResponse monChon;

                    if (loai == "Cơm") // Cơm không bị hạn chế
                    {
                        monChon = danhSachMon.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
                    }
                    else // Canh, Xào, Mặn bị hạn chế
                    {
                        monChon = danhSachMon
                            .Where(ma => !history[loai].Contains(ma.ID_TenMonAn)) // Lọc món chưa xuất hiện
                            .OrderBy(_ => Guid.NewGuid()) // Dựa vào Guild để sắp xếp rồi lấy phần tử đầu tiền
                            .FirstOrDefault();

                        // Cập nhật lịch sử khi chọn được món
                        if (monChon != null)
                        {
                            history[loai].Enqueue(monChon.ID_TenMonAn);
                            if (history[loai].Count > 9) // Kiểm tra hàng đợi ==9 vì 3 bữa mỗi ngày, mỗi bữa 1 món
                            {
                                history[loai].Dequeue();//Xóa phần tử VÀO đầu tiên để đảm bảo quy luật 3 ngày
                            }
                        }
                    }
                    if (monChon != null)
                    {


                        var ct_BuoiAn = await _ct_BuoiAnRepository.GetAllCT_BuoiAnByIdMonAn(monChon.ID_TenMonAn);
                        foreach (var item in ct_BuoiAn)
                        {
                            var tpDinhDuong = await _thanhPhanDinhDuongRepository.GetAllThanhPhanDinhDuongById(item.ID_ThucPham);
                            if (tpDinhDuong != null)
                            {
                                // Đảm bảo item.Gram không phải null, nếu có thể thì gán giá trị mặc định 0
                                double gram = item.Gram ?? 0;

                                thucDonNgay.TongNangLuong += ((tpDinhDuong?.Energy ?? 0) * gram) / 100;
                                thucDonNgay.TongProtein += ((tpDinhDuong?.Protein ?? 0) * gram) / 100;
                                thucDonNgay.TongChatBeo += ((tpDinhDuong?.Fat ?? 0) * gram) / 100;
                                thucDonNgay.TongCarbohydrate += ((tpDinhDuong?.Carbohydrate ?? 0) * gram) / 100;
                                thucDonNgay.TongChatXo += ((tpDinhDuong?.ChatXo ?? 0) * gram) / 100;
                                thucDonNgay.TongCanxi += ((tpDinhDuong?.Canxi ?? 0) * gram) / 100;
                                thucDonNgay.TongSat += ((tpDinhDuong?.Fe ?? 0) * gram) / 100;
                                thucDonNgay.TongMagie += ((tpDinhDuong?.Magie ?? 0) * gram) / 100;
                                thucDonNgay.TongPhotpho += ((tpDinhDuong?.Photpho ?? 0) * gram) / 100;
                                thucDonNgay.TongKali += ((tpDinhDuong?.Kali ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminC += ((tpDinhDuong?.VitaminC ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminB1 += ((tpDinhDuong?.VitaminB1 ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminB2 += ((tpDinhDuong?.VitaminB2 ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminA += ((tpDinhDuong?.VitaminA ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminD += ((tpDinhDuong?.VitaminD ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminE += ((tpDinhDuong?.VitaminE ?? 0) * gram) / 100;
                                thucDonNgay.TongVitaminK += ((tpDinhDuong?.VitaminK ?? 0) * gram) / 100;
                            }
                            else
                            {
                                // Nếu tpDinhDuong là null, có thể log lỗi hoặc bỏ qua phần tử này
                                // Log or handle the case where tpDinhDuong is null
                                Console.WriteLine($"No nutrition data found for ThucPham ID: {item.ID_ThucPham}");
                            }
                        }
                        buaAn.MonAn[loai] = monChon;
                    }
                }
                thucDonNgay.Buas.Add(buaAn);
            }
            return thucDonNgay;
        }



        public async Task<ThucDonTuanResponse> GenerateThucDonTuan()
        {
            var thucDonTuan = new ThucDonTuanResponse();
            
            var history = new Dictionary<string, Queue<int>>();

            // Khởi tạo lịch sử cho các loại món bị hạn chế
            foreach (var loai in new[] { "Canh", "Xào", "Mặn" })
            {
                history[loai] = new Queue<int>();
            }

            // Tạo thực đơn cho 7 ngày
            for (int day = 1; day <= 7; day++)
            {
                var thucDonNgay = await GenerateThucDonNgay(day, history);

                thucDonTuan.listThucDonNgay.Add(thucDonNgay);
                thucDonTuan.TongNangLuong += thucDonNgay.TongNangLuong;
                thucDonTuan.TongProtein += thucDonNgay.TongProtein;
                thucDonTuan.TongChatBeo += thucDonNgay.TongChatBeo;
                thucDonTuan.TongCarbohydrate += thucDonNgay.TongCarbohydrate;
                thucDonTuan.TongChatXo += thucDonNgay.TongChatXo;
                thucDonTuan.TongCanxi += thucDonNgay.TongCanxi;
                thucDonTuan.TongSat += thucDonNgay.TongSat;
                thucDonTuan.TongMagie += thucDonNgay.TongMagie;
                thucDonTuan.TongPhotpho += thucDonNgay.TongPhotpho;
                thucDonTuan.TongKali += thucDonNgay.TongKali;
                thucDonTuan.TongVitaminC += thucDonNgay.TongVitaminC;
                thucDonTuan.TongVitaminB1 += thucDonNgay.TongVitaminB1;
                thucDonTuan.TongVitaminB2 += thucDonNgay.TongVitaminB2;
                thucDonTuan.TongVitaminA += thucDonNgay.TongVitaminA;
                thucDonTuan.TongVitaminD += thucDonNgay.TongVitaminD;
                thucDonTuan.TongVitaminE += thucDonNgay.TongVitaminE;
                thucDonTuan.TongVitaminK += thucDonNgay.TongVitaminK;
            }
            return thucDonTuan;
        }

        #endregion

        #region Hàm tính độ fitness
        public async Task<double> Fitness(ThucDonTuanResponse thucDonTuan, int  maCheDoAn)
        {
            double fitness = 0;
            var cheDoAn = await _cheDoAnRepository.GetAllCheDoAnById(maCheDoAn);
            fitness += Math.Abs(thucDonTuan.TongNangLuong - cheDoAn.TongNangLuong);
            fitness += Math.Abs(thucDonTuan.TongProtein - cheDoAn.TongProtein);
            fitness += Math.Abs(thucDonTuan.TongChatBeo- cheDoAn.TongChatBeo);
            fitness += Math.Abs(thucDonTuan.TongNangLuong - cheDoAn.TongCarbohydrate);
            fitness += Math.Abs(thucDonTuan.TongNangLuong - cheDoAn.TongChatXo);
            fitness += Math.Abs(thucDonTuan .TongCanxi- cheDoAn.TongCanxi);
            fitness += Math.Abs(thucDonTuan .TongSat- cheDoAn.TongSat);
            fitness += Math.Abs(thucDonTuan .TongMagie- cheDoAn.TongMagie);
            fitness += Math.Abs(thucDonTuan.TongPhotpho - cheDoAn.TongPhotpho);
            fitness += Math.Abs(thucDonTuan.TongKali - cheDoAn.TongKali);
            fitness += Math.Abs(thucDonTuan .TongVitaminC- cheDoAn.TongVitaminC);
            fitness += Math.Abs(thucDonTuan.TongVitaminA- cheDoAn.TongVitaminA);
            fitness += Math.Abs(thucDonTuan.TongVitaminD - cheDoAn.TongVitaminD);
            fitness += Math.Abs(thucDonTuan.TongVitaminE - cheDoAn.TongVitaminE);
            fitness += Math.Abs(thucDonTuan.TongVitaminK-cheDoAn.TongVitaminK);
            return fitness;
        }
        #endregion

        #region Hàm khởi tạo quần thể (200 cá thể)
        public async Task<List<ThucDonTuanResponse>> KhoiTaoQuanThe()
        {
            List<ThucDonTuanResponse> quanThe = new List<ThucDonTuanResponse>();
            for(int i = 0; i < SoLuongQuanThe; i++)
            {
                var thucDonTuan = await GenerateThucDonTuan();
                quanThe.Add(thucDonTuan);
            }    
            return quanThe;
        }
        #endregion

        #region Hàm Chọn Lọc
        public async Task<List<ThucDonTuanResponse>> ChonLoc(List<ThucDonTuanResponse> quanThe, int maCheDoAn)
        {
            var quanTheChon = new List<ThucDonTuanResponse>();
            Random random = new Random();
            //Chọn số lượng cho quần thể mới
            for(int i = 0; i<SoLuongQuanTheChon;i++)
            {
                var caThe1 = quanThe[random.Next(quanThe.Count)];

                //Xử lý trường hợp trùng nhau
                var caThe2 = caThe1;
                while(caThe1 == caThe2)
                {
                    caThe2 = quanThe[random.Next(quanThe.Count)];
                }    
                double fitnessCaThe1 = await Fitness(caThe1, maCheDoAn);
                double fitnessCaThe2 = await Fitness(caThe2, maCheDoAn);
                if(fitnessCaThe1 > fitnessCaThe2)
                {
                    quanTheChon.Add(caThe1);
                }    
                else
                {
                    quanTheChon.Add(caThe2);
                }
            }    
            return quanTheChon;
        }
        #endregion

        #region Hàm Lai Ghép
        public async Task<ThucDonTuanResponse> LaiGhep(int maCheDoAn, ThucDonTuanResponse cha,ThucDonTuanResponse me)
        {
            Random random = new Random();
            var thucDonTuanCon1 = new ThucDonTuanResponse();
            var thucDonTuanCon2 = new ThucDonTuanResponse();
            var thucDonTuanMoi = new ThucDonTuanResponse();
            //Random vị trí ghép
            int viTriGhep = random.Next(1, 8);
            
            for(int i = 1; i <=7; i++)
            {
                if(i<=viTriGhep)
                {
                    thucDonTuanCon1.listThucDonNgay.Add(cha.listThucDonNgay[i - 1]);
                    thucDonTuanCon2.listThucDonNgay.Add(me.listThucDonNgay[i - 1]);
                }
                else
                {
                    thucDonTuanCon1.listThucDonNgay.Add(me.listThucDonNgay[i - 1]);
                    thucDonTuanCon2.listThucDonNgay.Add(cha.listThucDonNgay[i - 1]);
                }
            }

            //Cập nhật tổng độ dinh dưỡng
            foreach (var thucDonNgay in thucDonTuanCon1.listThucDonNgay)
            {
                thucDonTuanCon1.TongNangLuong += thucDonNgay.TongNangLuong;
                thucDonTuanCon1.TongProtein += thucDonNgay.TongProtein;
                thucDonTuanCon1.TongChatBeo += thucDonNgay.TongChatBeo;
                thucDonTuanCon1.TongCarbohydrate += thucDonNgay.TongCarbohydrate;
                thucDonTuanCon1.TongChatXo += thucDonNgay.TongChatXo;
                thucDonTuanCon1.TongCanxi += thucDonNgay.TongCanxi;
                thucDonTuanCon1.TongSat += thucDonNgay.TongSat;
                thucDonTuanCon1.TongMagie += thucDonNgay.TongMagie;
                thucDonTuanCon1.TongPhotpho += thucDonNgay.TongPhotpho;
                thucDonTuanCon1.TongKali += thucDonNgay.TongKali;
                thucDonTuanCon1.TongVitaminC += thucDonNgay.TongVitaminC;
                thucDonTuanCon1.TongVitaminB1 += thucDonNgay.TongVitaminB1;
                thucDonTuanCon1.TongVitaminB2 += thucDonNgay.TongVitaminB2;
                thucDonTuanCon1.TongVitaminA += thucDonNgay.TongVitaminA;
                thucDonTuanCon1.TongVitaminD += thucDonNgay.TongVitaminD;
                thucDonTuanCon1.TongVitaminE += thucDonNgay.TongVitaminE;
                thucDonTuanCon1.TongVitaminK += thucDonNgay.TongVitaminK;
            }
            
            foreach (var thucDonNgay in thucDonTuanCon2.listThucDonNgay)
            {
                thucDonTuanCon2.TongNangLuong += thucDonNgay.TongNangLuong;
                thucDonTuanCon2.TongProtein += thucDonNgay.TongProtein;
                thucDonTuanCon2.TongChatBeo += thucDonNgay.TongChatBeo;
                thucDonTuanCon2.TongCarbohydrate += thucDonNgay.TongCarbohydrate;
                thucDonTuanCon2.TongChatXo += thucDonNgay.TongChatXo;
                thucDonTuanCon2.TongCanxi += thucDonNgay.TongCanxi;
                thucDonTuanCon2.TongSat += thucDonNgay.TongSat;
                thucDonTuanCon2.TongMagie += thucDonNgay.TongMagie;
                thucDonTuanCon2.TongPhotpho += thucDonNgay.TongPhotpho;
                thucDonTuanCon2.TongKali += thucDonNgay.TongKali;
                thucDonTuanCon2.TongVitaminC += thucDonNgay.TongVitaminC;
                thucDonTuanCon2.TongVitaminB1 += thucDonNgay.TongVitaminB1;
                thucDonTuanCon2.TongVitaminB2 += thucDonNgay.TongVitaminB2;
                thucDonTuanCon2.TongVitaminA += thucDonNgay.TongVitaminA;
                thucDonTuanCon2.TongVitaminD += thucDonNgay.TongVitaminD;
                thucDonTuanCon2.TongVitaminE += thucDonNgay.TongVitaminE;
                thucDonTuanCon2.TongVitaminK += thucDonNgay.TongVitaminK;
            }

            //Tính độ fitness của 2 cá thể con
            double fitnessCon1 = await Fitness(thucDonTuanCon1, maCheDoAn);
            double fitnessCon2 = await Fitness(thucDonTuanCon2, maCheDoAn);
            if(fitnessCon1 < fitnessCon2)
            {
                thucDonTuanMoi = thucDonTuanCon1;
            }    
            else
            {
                thucDonTuanMoi = thucDonTuanCon2;
            }
            return thucDonTuanMoi;

        }

        #endregion

        #region Đột Biến
        public async Task<ThucDonTuanResponse> DotBien (ThucDonTuanResponse thucDonTuan)
        {
            var random = new Random();
            int ngayDotBien = random.Next(1, 8);

            //Lấy Thục Đơn ngày cần thay
            var thucDonNgayCanThay = thucDonTuan.listThucDonNgay.FirstOrDefault(ngay=>ngay.Ngay==ngayDotBien);
            if(thucDonNgayCanThay != null )
            {
                var history = new Dictionary<string, Queue<int>>();
                var thucDonNgayMoi = await GenerateThucDonNgay(ngayDotBien, history );
                thucDonTuan.listThucDonNgay.Remove(thucDonNgayCanThay);
                thucDonTuan.listThucDonNgay.Add(thucDonNgayMoi);
            }
            // Cập nhật lại tổng hợp dinh dưỡng cho ThucDonTuan
            thucDonTuan.TongNangLuong = thucDonTuan.listThucDonNgay.Sum(day => day.TongNangLuong);
            thucDonTuan.TongProtein = thucDonTuan.listThucDonNgay.Sum(day => day.TongProtein);
            thucDonTuan.TongChatBeo = thucDonTuan.listThucDonNgay.Sum(day => day.TongChatBeo);
            thucDonTuan.TongCarbohydrate = thucDonTuan.listThucDonNgay.Sum(day => day.TongCarbohydrate);
            thucDonTuan.TongChatXo = thucDonTuan.listThucDonNgay.Sum(day => day.TongChatXo);
            thucDonTuan.TongCanxi = thucDonTuan.listThucDonNgay.Sum(day => day.TongCanxi);
            thucDonTuan.TongSat = thucDonTuan.listThucDonNgay.Sum(day => day.TongSat);
            thucDonTuan.TongMagie = thucDonTuan.listThucDonNgay.Sum(day => day.TongMagie);
            thucDonTuan.TongPhotpho = thucDonTuan.listThucDonNgay.Sum(day => day.TongPhotpho);
            thucDonTuan.TongKali = thucDonTuan.listThucDonNgay.Sum(day => day.TongKali);
            thucDonTuan.TongVitaminC = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminC);
            thucDonTuan.TongVitaminB1 = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminB1);
            thucDonTuan.TongVitaminB2 = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminB2);
            thucDonTuan.TongVitaminA = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminA);
            thucDonTuan.TongVitaminD = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminD);
            thucDonTuan.TongVitaminE = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminE);
            thucDonTuan.TongVitaminK = thucDonTuan.listThucDonNgay.Sum(day => day.TongVitaminK);

            return thucDonTuan;
        }
        #endregion

        #region Tổng Hợp
        public async Task<ThucDonTuanResponse> ThuatToanGA (int maCheDoAn)
        {
            var thucDonTuan = new ThucDonTuanResponse();
            //Khởi tạo Quần Thể
            var quanThe = await KhoiTaoQuanThe();
            //Số lần chạy 
            for(int i = 0; i <SoLanChay; i++)
            {
                //Chọn lọc quần thể
                var chonQuanThe = await ChonLoc(quanThe, maCheDoAn);

                //Lai ghép quần thể
                var quanTheGhep= new List<ThucDonTuanResponse>();
                while(quanTheGhep.Count < SoLuongQuanTheChon)
                {
                    var cha = chonQuanThe[Random.Shared.Next(chonQuanThe.Count)];
                    var me = chonQuanThe[Random.Shared.Next(chonQuanThe.Count)];
                    var thucDonMoi = await LaiGhep(maCheDoAn, cha, me);
                    quanTheGhep.Add(thucDonMoi);
                }

                var quanTheDotBien = new List<ThucDonTuanResponse>();
                //Đột biến quần thể
                foreach(var caThe in quanTheGhep)
                {
                    var caTheDotBien = await DotBien(caThe);
                    quanTheDotBien.Add(caTheDotBien);
                }  
                quanThe = quanTheDotBien;
            }
            var caTheTotNhat = new ThucDonTuanResponse();
            
            double fitnessMin = double.MinValue;
            foreach(var caThe in quanThe)
            {
                var fitness = await Fitness(caThe, maCheDoAn);
                if(fitness > fitnessMin)
                {
                    fitnessMin = fitness;
                    caTheTotNhat = caThe;

                }    
            }
            return caTheTotNhat;

        }
        #endregion

    }
}
