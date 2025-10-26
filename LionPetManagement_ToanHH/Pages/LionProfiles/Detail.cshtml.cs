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
    public class DetailsModel : PageModel
    {
        private readonly ILionProfileService _pService;
        public DetailsModel(ILionProfileService pService)
        {
            _pService = pService;
        }

        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _pService.GetByIdAsync(id.Value);
            if (lionprofile == null)
            {
                return NotFound();
            }
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }
    }
}
