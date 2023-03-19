using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly DataContext _context;
        public StorageService(DataContext context)
        {
            _context = context;
        }

        //storageId, partId, row, column, drawer, countOfParts
        private static List<Storage> storageList = new List<Storage> {};

        public async Task<ServiceResponse<List<Storage>>> GetStorages()
        {
            var serviceResponse = new ServiceResponse<List<Storage>>();
            var dbStorage = await _context.Storages.ToListAsync();
            serviceResponse.Data = dbStorage;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Storage>>> GetStorageByPartId(int id)
        {
            var serviceResponse = new ServiceResponse<List<Storage>>();
            var dbStorage = await _context.Storages.ToListAsync();
            var stored = dbStorage.FindAll(s => s.partId == id);
            serviceResponse.Data = stored;
            return serviceResponse;
        }

        // public async Task<ServiceResponse<List<Storage>>> GetStorageByRow(int row)
        // {
        //     var serviceResponse = new ServiceResponse<List<Storage>>();
        //     var dbStorage = await _context.Storages.ToListAsync();
        //     var stored = dbStorage.FindAll(s => s.row == row);
        //     serviceResponse.Data = stored;
        //     return serviceResponse;
        // }

        // public async Task<ServiceResponse<List<Storage>>> GetStorageByColumn(int column)
        // {
        //     var serviceResponse = new ServiceResponse<List<Storage>>();
        //     var dbStorage = await _context.Storages.ToListAsync();
        //     var stored = dbStorage.FindAll(s => s.column == column);
        //     serviceResponse.Data = stored;
        //     return serviceResponse;
        // }

        // public async Task<ServiceResponse<List<Storage>>> GetStorageByDrawer(int drawer)
        // {
        //     var serviceResponse = new ServiceResponse<List<Storage>>();
        //     var dbStorage = await _context.Storages.ToListAsync();
        //     var stored = dbStorage.FindAll(s => s.drawer == drawer);
        //     serviceResponse.Data = stored;
        //     return serviceResponse;
        // }

        public async Task<ServiceResponse<List<Storage>>> AddStorage(Storage newStorage)
        {
            var serviceResponse = new ServiceResponse<List<Storage>>();
            _context.Storages.Add(newStorage);
            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Storage>> UpdateStorage(Storage updatedStorage)
        {
            var serviceResponse = new ServiceResponse<Storage>();
            var dbStorage = await _context.Storages.ToListAsync();

            try {
                var stored = dbStorage.FirstOrDefault(s => s.storageId == updatedStorage.storageId);
                if(stored is null) {
                    throw new Exception($"Part with Id '{updatedStorage.storageId}' not found.");
                }

                //storageId, partId, row, column, drawer, countOfParts
                stored.partId = updatedStorage.partId;
                stored.row = updatedStorage.row;
                stored.column = updatedStorage.column;
                stored.drawer = updatedStorage.drawer;
                stored.countOfParts = updatedStorage.countOfParts;
            
                serviceResponse.Data = stored;
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Storage>>> DeleteStorage(int id)
        {
            var serviceResponse = new ServiceResponse<List<Storage>>();
            var dbStorage = await _context.Storages.ToListAsync();

            try {
                var stored = dbStorage.FirstOrDefault(s => s.storageId == id);
                if(stored is null) {
                    throw new Exception($"Storage with Id '{id}' not found.");
                }

                _context.Storages.Remove(stored);
            
                serviceResponse.Data = storageList;
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }

        
    }
}