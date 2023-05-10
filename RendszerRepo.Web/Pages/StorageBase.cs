using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Web.Services;
using RendszerRepo.Web.Services.Contracts;

namespace RendszerRepo.Web.Pages
{
    public class StorageBase : ComponentBase
    {
        [Inject]
        public FEIStorageService FEStorageService { get; set; }

        public ServiceResponse<List<GetStoragesDto>> Storages { get; set; }
        protected UpdateStoragesDto UpdatedStorage { get; set; } = new UpdateStoragesDto();

        protected UpdateMaxDto UpdatedMax { get; set; } = new UpdateMaxDto();
        protected AddStorageDto NewStorage { get; set; } = new AddStorageDto();
        protected bool ShowSuccessMessage { get; set; } = false;
        protected bool ShowErrorMessage { get; set; } = false;
        protected string SuccessMessage { get; set; }
        protected string ErrorMessage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task AddNewStorage()
        {
            var response = await FEStorageService.AddStorage(NewStorage);

            if (response.Success)
            {
                NavigationManager.NavigateTo("/storage");
                ShowSuccessMessage = true;
                SuccessMessage = "Storage added successfully!";
                NewStorage = new AddStorageDto();

            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }
        protected async Task DeleteStorage(int storageId)
        {
            var response = await FEStorageService.DeleteStorage(storageId);

            if (response.Success)
            {
                Storages = await FEStorageService.GetStorages();
                ShowSuccessMessage = true;
                SuccessMessage = "Storage deleted successfully!";

                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Error");
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }
        protected async Task UpdateStorage(GetStoragesDto storage)
        {
            var response = await FEStorageService.UpdateStorage(new UpdateStoragesDto
            {
                storageId = storage.storageId,
                partId = storage.partId,
                row = storage.row,
                column = storage.column,
                drawer = storage.drawer,
                countOfParts = storage.countOfParts,
                max = storage.max
            });

            if (response.Success)
            {
                ShowSuccessMessage = true;
                SuccessMessage = "Storage updated successfully!";
                NavigationManager.NavigateTo("/storage");
            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }

        protected async Task UpdateMax(GetStoragesDto storage)
        {
            var response = await FEStorageService.UpdateMax(new UpdateMaxDto
            {
                storageId = storage.storageId,
                max = storage.max
            });

            if (response.Success)
            {
                ShowSuccessMessage = true;
                SuccessMessage = "Storage updated successfully!";
                NavigationManager.NavigateTo("/storage");
            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Storages = await FEStorageService.GetStorages();
        }
    }
}
