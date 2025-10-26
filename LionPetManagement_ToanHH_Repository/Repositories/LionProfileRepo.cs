using LionPetManagement_ToanHH_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionPetManagement_ToanHH_Repository.Repositories
{
    public class LionProfileRepo : GenericRepository<LionProfile>
    {
        public LionProfileRepo() { }
        public LionProfileRepo(SU25LionDBContext context) => _context = context;

        public new async Task<List<LionProfile>?> GetAllAsync()
            => await _context.LionProfiles.Include(b => b.LionType).ToListAsync();

        public new async Task<LionProfile?> GetByIdAsync(int id)
           => await _context.LionProfiles
               .Include(b => b.LionType)
               .FirstOrDefaultAsync(b => b.LionProfileId == id);

        public async Task<(List<LionProfile> items, int totalPages)> SearchAsyncWithPagination(double? weight, string lionTypeName, int page = 1, int pageSize = 10)
        {
            var query = _context.LionProfiles.AsQueryable();

            if (weight > 0)
            {
                query = query.Where(b => b.Weight == weight);
            }

            if (!string.IsNullOrEmpty(lionTypeName))
            {
                query = query.Where(b => b.LionType.LionTypeName.ToLower().Contains(lionTypeName.ToLower()));
            }

            var totalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            var item = await query
                .Include(b => b.LionType)
                .OrderByDescending(b => b.ModifiedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (item, totalPages);
        }
    }
}
