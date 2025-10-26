using LionPetManagement_ToanHH_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Repository.Repositories
{

    public class LionTypeRepo : GenericRepository<LionType>
    {
        public LionTypeRepo() { }
        public LionTypeRepo(SU25LionDBContext context) => _context = context;
    }
}
