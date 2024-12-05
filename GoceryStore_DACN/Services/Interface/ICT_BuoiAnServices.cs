using GoceryStore_DACN.DTOs;
using GoceryStore_DACN.Models.Respones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore_DACN.Repositories.Interface
{
    public interface ICT_BuoiAnServices
    {
        // Định nghĩa các phương thức của interface tại đây
        public Task<IEnumerable<CT_BuoiAnDTO>> GetAllCT_BuoiAnDTO ();
        public Task<List<CT_BuoiAnResponse>> GetAllCT_BuoiAnByIDMonAn (int id);
    }
}
