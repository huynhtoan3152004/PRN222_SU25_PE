using LionPetManagement_ToanHH_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Service
{
    public interface ILionTypeService
    {
        Task<List<LionType>> GetAllAsync();
    }
}
