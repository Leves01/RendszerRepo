using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.StorageService
{
    public interface IStorageService
    {
        Task<ServiceResponse<List<Storage>>> GetStorages();
        Task<ServiceResponse<List<Storage>>> GetStorageByPartId(int id);

        // Task<ServiceResponse<List<Storage>>> GetStorageByRow(int row);
        // Task<ServiceResponse<List<Storage>>> GetStorageByColumn(int column);
        // Task<ServiceResponse<List<Storage>>> GetStorageByDrawer(int drawer);
        
        Task<ServiceResponse<List<Storage>>> AddStorage(Storage newStorage);
        Task<ServiceResponse<Storage>> UpdateStorage(Storage updatedStorage);
        Task<ServiceResponse<List<Storage>>> DeleteStorage(int id);
    }
}