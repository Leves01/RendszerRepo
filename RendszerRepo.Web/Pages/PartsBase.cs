using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Reserves;
using RendszerRepo.Web.Services.Contracts;
using System.Data;

namespace RendszerRepo.Web.Pages
{
    public class PartsBase : ComponentBase
    {
        [Inject]
        public FEIPartService FEPartService { get; set; }

        public ServiceResponse<List<GetPartDto>> Parts { get; set; }

        protected AddPartDto NewPart { get; set; } = new AddPartDto();

        protected UpdatePartDto UpdatedPart { get; set; } = new UpdatePartDto();

        protected bool ShowSuccessMessage { get; set; } = false;
        protected bool ShowErrorMessage { get; set; } = false;
        protected string SuccessMessage { get; set; }
        protected string ErrorMessage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task AddNewPart()
		{
			var response = await FEPartService.AddPart(NewPart);

			if (response.Success)
			{
                NavigationManager.NavigateTo("/parts");
                ShowSuccessMessage = true;
				SuccessMessage = "Part added successfully!";
				NewPart = new AddPartDto();
                
            }
			else
			{
				ShowErrorMessage = true;
				ErrorMessage = response.Message;
			}
		}

        protected async Task DeletePart(int partId)
        {
            var response = await FEPartService.DeletePart(partId);

            if (response.Success)
            {
                Parts = await FEPartService.GetAllParts();
                ShowSuccessMessage = true;
                SuccessMessage = "Part deleted successfully!";

                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Error");
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }

        protected async Task UpdatePart(GetPartDto part)
        {
            var response = await FEPartService.UpdatePart(new UpdatePartDto
            {
                partId = part.partId,
                partName = part.partName,
                price = part.price,
                maxCount = part.maxCount
            });

            if (response.Success)
            {
                ShowSuccessMessage = true;
                SuccessMessage = "Part updated successfully!";
                NavigationManager.NavigateTo("/parts");
            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }

        protected override async Task OnInitializedAsync()
		{
			Parts = await FEPartService.GetAllParts();
        }

	}
}
