using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.StorageService
{
    public interface IStorageService
    {
        Task<ServiceResponse<List<GetStoragesDto>>> GetStorages();
        Task<ServiceResponse<List<GetStoragesDto>>> GetStorageByPartId(int id);

        // Task<ServiceResponse<List<Storage>>> GetStorageByRow(int row);
        // Task<ServiceResponse<List<Storage>>> GetStorageByColumn(int column);
        // Task<ServiceResponse<List<Storage>>> GetStorageByDrawer(int drawer);
        
        Task<ServiceResponse<List<GetStoragesDto>>> AddStorage(AddStorageDto newStorage);
        Task<ServiceResponse<GetStoragesDto>> UpdateStorage(UpdateStoragesDto updatedStorage);
        Task<ServiceResponse<List<GetStoragesDto>>> DeleteStorage(int id);

        //B - 6
        Task<ServiceResponse<GetStoragesDto>> UpdateMax(UpdateMaxDto updateMax);
    }
}