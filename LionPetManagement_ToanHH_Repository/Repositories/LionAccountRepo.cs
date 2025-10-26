using LionPetManagement_ToanHH_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Repository.Repositories
{
    public class LionAccountRepo : GenericRepository<LionAccount>
    {
        public LionAccountRepo() { }
        public LionAccountRepo(SU25LionDBContext context) => _context = context;

        public async Task<LionAccount> GetUserAccountAsync(string email, string password)
            => await _context.LionAccounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }
}
