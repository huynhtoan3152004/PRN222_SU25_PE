using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LionPetManagement_ToanHH_Repository;
using LionPetManagement_ToanHH_Repository.Models;
using LionPetManagement_ToanHH_Service;
using Microsoft.AspNetCore.Authorization;

namespace LionPetManagement_ToanHH.Pages.LionProfiles
{
    [Authorize(Roles = "2")]
    public class UpdateModel : PageModel
    {
        private readonly ILionProfileService _pService;
        private readonly ILionTypeService _tService;
        
        public UpdateModel(ILionProfileService pService, ILionTypeService tService)
        {
            _pService = pService;
            _tService = tService;
        }

        [BindProperty]
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
            LionProfile = lionprofile;
            ViewData["LionTypeId"] = new SelectList(await _tService.GetAllAsync(), "LionTypeId", "LionTypeName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["LionTypeId"] = new SelectList(await _tService.GetAllAsync(), "LionTypeId", "LionTypeName");
                return Page();
            }

            try
            {
                // Sửa: Phải gọi UpdateAsync chứ không phải CreateAsync
                LionProfile.ModifiedDate = DateTime.Now;
                var result = await _pService.UpdateAsync(LionProfile);

                if (result <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Không thể cập nhật hồ sơ.");
                    ViewData["LionTypeId"] = new SelectList(await _tService.GetAllAsync(), "LionTypeId", "LionTypeName");
                    return Page();
                }

                TempData["SuccessMessage"] = "Cập nhật hồ sơ thành công!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LionProfileExists(LionProfile.LionProfileId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<bool> LionProfileExists(int id)
        {
            var profile = await _pService.GetByIdAsync(id);
            return profile != null;
        }
    }
}
