using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LionPetManagement_ToanHH_Repository;
using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Service;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_ToanHH.Pages.LionProfiles
{
    [Authorize(Roles="2")]
    public class CreateModel : PageModel
    {
        private readonly ILionProfileService _lionProfileService;
        private readonly ILionTypeService _lionTypeService;
        public CreateModel(ILionProfileService lionAccountService, ILionTypeService lionTypeService)
        {
            _lionProfileService = lionAccountService;
            _lionTypeService = lionTypeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["LionTypeId"] = new SelectList(await _lionTypeService.GetAllAsync(), "LionTypeId", "LionTypeName");
            return Page();
        }

        [BindProperty]
        public LionProfile LionProfile { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LionTypeId"] = new SelectList(await _lionTypeService.GetAllAsync(), "LionTypeId", "LionTypeName");
                return Page();
            }

            var result = await _lionProfileService.CreateAsync(LionProfile);
            if (result <= 0)
            {
                ViewData["LionTypeId"] = new SelectList(await _lionTypeService.GetAllAsync(), "LionTypeId", "LionTypeName");
                ModelState.AddModelError(string.Empty, "Unable to create Lion Profile. Please try again.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
