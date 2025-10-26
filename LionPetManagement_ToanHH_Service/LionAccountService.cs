using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Repository.Repositories;
using LionPetManagement_ToanHH_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Service
{
    public class LionAccountService : ILionAccountService
    {
        private readonly LionAccountRepo _lionAccount;
        public LionAccountService()
        {
            _lionAccount = new LionAccountRepo();
        }
        public async Task<LionAccount?> GetUserAccountAsync(string email, string password)
        {
            var user = await _lionAccount.GetUserAccountAsync(email, password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
