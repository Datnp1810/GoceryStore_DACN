﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoceryStore_DACN.Entities
{
    public class CT_BuoiAn
    {
        public int ID_ThucPham { get; set; }
        public int ID_MonAn { get; set; }
        public double Gram { get; set; }
        // Quan hệ một-nhiều với Chế Độ Ăn

        public ThucPham ThucPham { get; set; }
        public MonAn MonAn { get; set; }
    }
}
