using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Reserves;
using RendszerRepo.Web.Services.Contracts;

namespace RendszerRepo.Web.Pages
{
    public class ReservesBase : ComponentBase
    {
        [Inject]
        public FEIPartService FEPartService { get; set; }
        public ServiceResponse<List<GetReserveDto>> Reserves { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Reserves = await FEPartService.getReserves();
        }
    }
}
