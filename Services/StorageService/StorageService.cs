using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public StorageService(DataContext context, IMapper mapper)
        {        
            _context = context;
            _mapper = mapper;
        }

        //storageId, partId, row, column, drawer, countOfParts
        private static List<Storage> storageList = new List<Storage> {};

        public async Task<ServiceResponse<List<GetStoragesDto>>> GetStorages()
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();
            serviceResponse.Data = dbStorage.Select(s => _mapper.Map<GetStoragesDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStoragesDto>>> GetStorageByPartId(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();
            var stored = dbStorage.FindAll(s => s.partId == id);
            //ez fogalmam sincs hogy müködik-e mert nem tudok rendesen tesztelni laptopon
            serviceResponse.Data = dbStorage.Select(s => _mapper.Map<GetStoragesDto>(stored)).ToList();
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

        public async Task<ServiceResponse<List<GetStoragesDto>>> AddStorage(AddStorageDto newStorage)
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();

            var storageNotAvailable = dbStorage.FirstOrDefault(s => (s.column == newStorage.column && s.drawer == newStorage.drawer && s.row == newStorage.row));

            if(storageNotAvailable is not null) {
                // ez nem tudom hogy jelenik meg, szintén le kell tesztelni
                throw new Exception($"An item in '{newStorage.column}, {newStorage.drawer}, {newStorage.row}' already exists.");
            } else {
                _context.Storages.Add(_mapper.Map<Storage>(newStorage));
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoragesDto>> UpdateStorage(UpdateStoragesDto updatedStorage)
        {
            var serviceResponse = new ServiceResponse<GetStoragesDto>();
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
            
                serviceResponse.Data = _mapper.Map<GetStoragesDto>(stored);
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStoragesDto>>> DeleteStorage(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();

            try {
                var stored = dbStorage.FirstOrDefault(s => s.storageId == id);
                if(stored is null) {
                    throw new Exception($"Storage with Id '{id}' not found.");
                }

                dbStorage.Remove(stored);

                
            
                serviceResponse.Data = dbStorage.Select(s => _mapper.Map<GetStoragesDto>(s)).ToList();
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }

        
    }
}