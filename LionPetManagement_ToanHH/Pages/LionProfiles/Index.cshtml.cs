using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_ToanHH_Repository;
using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Service;

namespace LionPetManagement_ToanHH.Pages.LionProfiles
{
    public class IndexModel : PageModel
    {
        private readonly ILionProfileService _service;

        public IndexModel(ILionProfileService service)
        {
            _service = service;
        }


        public IList<LionProfile> LionProfile { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public double? Weight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? LionTypeName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; } = 1;

        public async Task OnGetAsync()
        {
            // Get all profiles first
            var searchItem = await _service.SearchAsyncWithPagination(Weight, LionTypeName, PageNumber, PageSize);
            LionProfile = searchItem.items;
            if (searchItem.totalPages > 0)
            {
                TotalPages = searchItem.totalPages;
            }
            else
            {
                TotalPages = 1;
            }
        }
    }
}
