using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Service
{
    public class LionTypeService : ILionTypeService
    {
        private readonly LionTypeRepo _lionTypeRepository;
        public LionTypeService()
        {
            _lionTypeRepository = new LionTypeRepo();
        }
        public Task<List<LionType>> GetAllAsync()
        {
            return _lionTypeRepository.GetAllAsync();
        }
    }
}
