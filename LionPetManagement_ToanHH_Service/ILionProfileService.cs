using LionPetManagement_ToanHH_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Service
{
    public interface ILionProfileService
    {
        Task<List<LionProfile>> GetAllAsync();
        Task<LionProfile?> GetByIdAsync(int id);
        Task<(List<LionProfile> items, int totalPages)> SearchAsyncWithPagination(double? weight, string lionTypeName, int page = 1, int pageSize = 10);
        Task<int> CreateAsync(LionProfile entity);
        Task<int> UpdateAsync(LionProfile entity);
        Task<bool> RemoveAsync(LionProfile entity);
    }
}
