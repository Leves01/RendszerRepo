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

        //mapping probléma, majd megnézem
        public async Task<ServiceResponse<List<GetStoragesDto>>> GetStorageByPartId(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();
            var stored = dbStorage.FindAll(s => s.partId == id);
            serviceResponse.Data = dbStorage.Select(s => _mapper.Map<GetStoragesDto>(stored)).ToList();
            return serviceResponse;
        }

        //B5 - Beérkező anyagok felvétele a rendszerben, a tárolás, a rekesz meghatározása
        public async Task<ServiceResponse<List<GetStoragesDto>>> AddStorage(AddStorageDto newStorage)
        {
            var serviceResponse = new ServiceResponse<List<GetStoragesDto>>();
            var dbStorage = await _context.Storages.ToListAsync();

            var storageNotAvailable = dbStorage.FirstOrDefault(s => (s.column == newStorage.column && s.drawer == newStorage.drawer && s.row == newStorage.row));

            if(storageNotAvailable is not null) {
                throw new Exception($"An item in column: '{newStorage.column}', drawer: '{newStorage.drawer}', row: '{newStorage.row}' already exists.");
            } else if (newStorage.max < newStorage.countOfParts){
                throw new Exception($"The maximum is lower than the count of parts you are adding");
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
                } else if (updatedStorage.max < updatedStorage.countOfParts) {
                    throw new Exception($"The maximum is lower than the count of parts you are updating");
                }

                //storageId, partId, row, column, drawer, countOfParts
                stored.partId = updatedStorage.partId;
                stored.countOfParts = updatedStorage.countOfParts;
                stored.max = updatedStorage.max;
            
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

                _context.Storages.Remove(stored);

                serviceResponse.Data = dbStorage.Select(s => _mapper.Map<GetStoragesDto>(s)).ToList();
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }

        //B6 - Rekeszeknél a maximálisan elhelyezhető darabszám kezelése
        public async Task<ServiceResponse<GetStoragesDto>> UpdateMax(UpdateMaxDto updateMax)
        {
            var serviceResponse = new ServiceResponse<GetStoragesDto>();
            var dbStorage = await _context.Storages.ToListAsync();

            try {
                var stored = dbStorage.FirstOrDefault(s => s.storageId == updateMax.storageId);
                if(stored is null) {
                    throw new Exception($"Storage with Id '{updateMax.storageId}' not found.");
                } else if (updateMax.max < stored.countOfParts) {
                    throw new Exception($"The maximum can't be lower than the number of currently stored parts");
                } else {
                    //storageId, partId, row, column, drawer, countOfParts
                    stored.max = updateMax.max;
                }
  
                serviceResponse.Data = _mapper.Map<GetStoragesDto>(stored);

            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }
    }
}