using System.ComponentModel.DataAnnotations;

namespace GoceryStore_DACN.Models.Respones
{
    public class CheDoAnResponse
    {
        public int ID_CDA { get; set; }

        public string TenCheDoAn { get; set; }

        // Tổng thành phần dinh dưỡng
        public double TongNangLuong { get; set; } // Tổng Năng lượng (Kcal)
        public double TongProtein { get; set; } // Tổng Protein (g)
        public double TongChatBeo { get; set; } // Tổng Chất béo (g)
        public double TongCarbohydrate { get; set; } // Tổng Carbohydrate (g)
        public double TongChatXo { get; set; } // Tổng Chất xơ (g)
        public double TongCanxi { get; set; } // Tổng Canxi (mg)
        public double TongSat { get; set; } // Tổng Sắt (mg)
        public double TongMagie { get; set; } // Tổng Magiê (mg)
        public double TongPhotpho { get; set; } // Tổng Phốt pho (mg)
        public double TongKali { get; set; } // Tổng Kali (mg)
        public double TongVitaminC { get; set; } // Tổng Vitamin C (mg)
        public double TongVitaminB1 { get; set; } // Tổng Vitamin B1 (mg)
        public double TongVitaminB2 { get; set; } // Tổng Vitamin B2 (mg)
        public double TongVitaminA { get; set; } // Tổng Vitamin A (μg)
        public double TongVitaminD { get; set; } // Tổng Vitamin D (μg)
        public double TongVitaminE { get; set; } // Tổng Vitamin E (mg)
        public double TongVitaminK { get; set; } // Tổng Vitamin K (μg)
    }
}
