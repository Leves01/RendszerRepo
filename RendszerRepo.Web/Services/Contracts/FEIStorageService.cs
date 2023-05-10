using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;

namespace RendszerRepo.Web.Services.Contracts
{
    public interface FEIStorageService
    {
        Task<ServiceResponse<List<GetStoragesDto>>> GetStorages();
        Task<ServiceResponse<List<GetStoragesDto>>> GetStorageByPartId(int id);
        Task<ServiceResponse<List<GetStoragesDto>>> AddStorage(AddStorageDto newStorage);
        Task<ServiceResponse<GetStoragesDto>> UpdateStorage(UpdateStoragesDto updatedStorage);
        Task<ServiceResponse<List<GetStoragesDto>>> DeleteStorage(int id);
        Task<ServiceResponse<GetStoragesDto>> UpdateMax(UpdateMaxDto updateMax);
    }
}
