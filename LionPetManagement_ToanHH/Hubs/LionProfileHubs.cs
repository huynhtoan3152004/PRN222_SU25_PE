using LionPetManagement_ToanHH_Service;
using Microsoft.AspNetCore.SignalR;

namespace LionPetManagement_ToanHH.Hubs
{
    public class LionProfileHubs : Hub
    {
        private readonly ILionProfileService _lionProfileService;
        public LionProfileHubs(ILionProfileService lionProfileService)
        {
            _lionProfileService = lionProfileService;
        }

        public async Task HubDelete(string id)
        {
            if (int.TryParse(id, out int lionProfileId))
            {
                var lionProfile = await _lionProfileService.GetByIdAsync(lionProfileId);
                if (lionProfile != null)
                {
                    var result = await _lionProfileService.RemoveAsync(lionProfile);
                    if (result)
                    {
                        await Clients.All.SendAsync("ReceiveDelete", id);
                    }
                }
            }
        }
    }
}
