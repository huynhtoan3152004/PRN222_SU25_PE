using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Service
{
    public class LionProfileService : ILionProfileService
    {
        private readonly LionProfileRepo _lionProfileRepo;
        public LionProfileService()
        {
            _lionProfileRepo = new LionProfileRepo();
        }

        public Task<List<LionProfile>> GetAllAsync()
        {
            return _lionProfileRepo.GetAllAsync();
        }

        public Task<LionProfile?> GetByIdAsync(int id)
        {
            return _lionProfileRepo.GetByIdAsync(id);
        }

        public Task<(List<LionProfile> items, int totalPages)> SearchAsyncWithPagination(double? weight, string lionTypeName, string lionName, int page = 1, int pageSize = 10)
        {
            return _lionProfileRepo.SearchAsyncWithPagination(weight, lionTypeName, lionName, page, pageSize);
        }

        public Task<int> CreateAsync(LionProfile entity)
        {
            return _lionProfileRepo.CreateAsync(entity);
        }

        public Task<int> UpdateAsync(LionProfile entity)
        {
            return _lionProfileRepo.UpdateAsync(entity);
        }

        public Task<bool> RemoveAsync(LionProfile entity)
        {
            return _lionProfileRepo.RemoveAsync(entity);
        }
    }
}
