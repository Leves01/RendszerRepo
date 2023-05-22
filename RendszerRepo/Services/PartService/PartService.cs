using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.PartService
{
    public class PartService : IPartService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PartService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //partId, partName, price, maxCount
        private static List<Part> parts = new List<Part> {};

        public async Task<ServiceResponse<List<GetPartDto>>> GetAllParts()
        {
            var serviceResponse = new ServiceResponse<List<GetPartDto>>();
            var dbParts = await _context.Parts.ToListAsync();
            serviceResponse.Data = dbParts.Select(p => _mapper.Map<GetPartDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPartDto>>> AddPart(AddPartDto newPart)
        {
            var serviceResponse = new ServiceResponse<List<GetPartDto>>();
            var dbParts = await _context.Parts.ToListAsync();

            var addedPart = dbParts.FirstOrDefault(u => (u.partName == newPart.partName));

            if(addedPart is not null) {
                throw new Exception($"'{newPart.partName}' part already exists.");
            }
            else {
                _context.Parts.Add(_mapper.Map<Part>(newPart));
            }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart)
        {
            var serviceResponse = new ServiceResponse<GetPartDto>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.FirstOrDefault(p => p.partId == updatedPart.partId);
                if(part is null) {
                    throw new Exception($"Part with Id '{updatedPart.partId}' not found.");
                }

                part.price = updatedPart.price;
            
                serviceResponse.Data = _mapper.Map<GetPartDto>(part);
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPartDto>> UpdatePart(UpdatePartDto updatedPart)
        {
            var serviceResponse = new ServiceResponse<GetPartDto>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.FirstOrDefault(p => p.partId == updatedPart.partId);
                if(part is null) {
                    throw new Exception($"Part with Id '{updatedPart.partId}' not found.");
                }

                part.partName = updatedPart.partName;
                part.price = updatedPart.price;
                part.maxCount = updatedPart.maxCount;
            
                serviceResponse.Data = _mapper.Map<GetPartDto>(part);
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPartDto>> DeletePart(int id)
        {
            var serviceResponse = new ServiceResponse<GetPartDto>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.First(p => p.partId == id);
                if(part is null) {
                    throw new Exception($"Part with Id '{id}' not found.");
                }

                _context.Parts.Remove(part);
            
                serviceResponse.Data = _mapper.Map<GetPartDto>(part);
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPartDto>> PartToProject(PartToProjectDto newPartToProject)
        {
            var serviceResponse = new ServiceResponse<GetPartDto>();
            var dbParts = await _context.Parts.ToListAsync();
            var dbPrProp = await _context.ProjectProperties.ToListAsync();

            try {
                var part = dbParts.First(p => p.partId == newPartToProject.partId);
                var project = dbPrProp.First(u => (u.ProjectId == newPartToProject.ProjectId));

                if(part is null) {
                    throw new Exception($"Part with Id '{newPartToProject.partId}' not found.");
                }
                if(project is null) {
                    throw new Exception($"Project with Id '{newPartToProject.ProjectId}' not found.");
                }

                project.partId = newPartToProject.partId;
                project.quantity = newPartToProject.quantity;
                
                serviceResponse.Data = _mapper.Map<GetPartDto>(project);
                await PartOutOfStorage(newPartToProject.ProjectId, newPartToProject.partId, newPartToProject.quantity);
            } 
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetStoragesDto>> PartOutOfStorage(int selectedProjectId, int selectedPartId, int selectedQuantity)
        {
            var serviceResponse = new ServiceResponse<GetStoragesDto>();
            var dbStorage = await _context.Storages.ToListAsync();
            var dbProjects = await _context.ProjectProperties.ToListAsync();

            try{
                var storage = dbStorage.First(p => p.partId == selectedPartId);
                var projects = dbProjects.First(p => p.ProjectId == selectedProjectId);
                if((storage.countOfParts-selectedQuantity)<0) {
                    // throw new Exception($"Not enough parts.");
                    var resDto = new AddReserveDto() 
                    {
                        projectId = selectedProjectId,
                        partId = selectedPartId,
                        neededAmount = Math.Abs(storage.countOfParts-selectedQuantity)
                    };

                    await addReserves(resDto);
                }
                else {
                    storage.countOfParts-=selectedQuantity;
                }
                    
                serviceResponse.Data = _mapper.Map<GetStoragesDto>(storage);
            }
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReserveDto>>> getReserves()
        {
            var serviceResponse = new ServiceResponse<List<GetReserveDto>>();
            var dbReserves = await _context.Reserves.ToListAsync();
            serviceResponse.Data = dbReserves.Select(r => _mapper.Map<GetReserveDto>(r)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReserveDto>>> addReserves(AddReserveDto newReserve)
        {
            var serviceResponse = new ServiceResponse<List<GetReserveDto>>();
            var dbReserve = await _context.Reserves.ToListAsync();

            _context.Reserves.Add(_mapper.Map<reservedParts>(newReserve));
            
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
    }
}