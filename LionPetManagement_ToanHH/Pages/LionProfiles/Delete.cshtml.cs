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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using LionPetManagement_ToanHH.Hubs;

namespace LionPetManagement_ToanHH.Pages.LionProfiles
{
    [Authorize(Roles = "2")]
    public class DeleteModel : PageModel
    {
        private readonly ILionProfileService _pService;
        private readonly IHubContext<LionProfileHubs> _hubContext;
        public DeleteModel(ILionProfileService profileService, IHubContext<LionProfileHubs> hubContext)
        {
            _pService = profileService;
            _hubContext = hubContext;
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
            else
            {
                LionProfile = lionprofile;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _pService.GetByIdAsync(id.Value);
            if (lionprofile != null)
            {
                var result = await _pService.RemoveAsync(lionprofile);
                if (result)
                {
                    // Notify all clients about the deletion
                    await _hubContext.Clients.All.SendAsync("ReceiveDelete", id.Value.ToString());
                }
                else
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteRegularAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lionprofile = await _pService.GetByIdAsync(id.Value);
            if (lionprofile != null)
            {
                var result = await _pService.RemoveAsync(lionprofile);
                if (!result)
                {
                    return Page();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
