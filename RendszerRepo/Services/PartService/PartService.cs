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
            var dbProjects = await _context.Project.ToListAsync();

            try {
                var part = dbParts.First(p => p.partId == newPartToProject.partId);
                var project = dbProjects.First(u => (u.ProjectId == newPartToProject.ProjectId));

                if(part is null) {
                    throw new Exception($"Part with Id '{newPartToProject.partId}' not found.");
                }
                if(project is null) {
                    throw new Exception($"Project with Id '{newPartToProject.ProjectId}' not found.");
                }  
                
                await PartOutOfStorage(newPartToProject.userId, newPartToProject.ProjectId, newPartToProject.partId, newPartToProject.quantity);

                serviceResponse.Data = _mapper.Map<GetPartDto>(project);
            } 
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStoragesDto>> PartOutOfStorage(int selectedUserId, int selectedProjectId, int selectedPartId, int selectedQuantity)
        {
            var serviceResponse = new ServiceResponse<GetStoragesDto>();
            var dbStorage = await _context.Storages.ToListAsync();
            var dbProjects = await _context.Project.ToListAsync();

            try{
                var storage = dbStorage.First(p => p.partId == selectedPartId);
                var projects = dbProjects.First(p => p.ProjectId == selectedProjectId);
                if((storage.countOfParts-selectedQuantity)<0) {

                    var resDto = new AddReserveDto() 
                    {
                        projectId = selectedProjectId,
                        partId = selectedPartId,
                        neededAmount = Math.Abs(storage.countOfParts-selectedQuantity)
                    };

                    await addReserves(resDto);

                    storage.countOfParts = 0;
                }
                else {

                    storage.countOfParts-=selectedQuantity;

                    var projectDto = new PartToProjectDto() 
                    {
                        userId = selectedUserId, //Donát itt majd frontedről légyszi kérd be a user idjét
                        ProjectId = selectedProjectId,
                        partId = selectedPartId,
                        quantity = selectedQuantity
                    };

                    _context.ProjectProperties.Add(_mapper.Map<Project_properties>(projectDto));
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

        public async Task<ServiceResponse<GetReserveDto>> updateReserve(UpdateReserveDto updateReserve)
        {
            var serviceResponse = new ServiceResponse<GetReserveDto>();
            var dbReserves = await _context.Reserves.ToListAsync();

            try {
                var reserve = dbReserves.FirstOrDefault(p => p.reservedPartsId == updateReserve.reservedPartsId);
                if(reserve is null) {
                    throw new Exception($"Reserve with Id '{updateReserve.reservedPartsId}' not found.");
                }

                reserve.neededAmount = updateReserve.neededAmount;
            
                serviceResponse.Data = _mapper.Map<GetReserveDto>(reserve);

            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        //ha ez megvan hívva, akkor a storageből kiszedi megint a dolgokat és eltünteni a reservet ha elegendő
        public async Task<ServiceResponse<GetStoragesDto>> fillReserves(int reservedId, int partId, int projectId) //ez itt amúgy megkapja a frontendből ha minden igaz
        {
            var serviceResponse = new ServiceResponse<GetStoragesDto>();
            var dbReserve = await _context.Reserves.ToListAsync();
            var dbStorage = await _context.Storages.ToListAsync();
            var dbProjects = await _context.Project.ToListAsync();

            try{
                var reserve = dbReserve.First(p => p.reservedPartsId == reservedId);
                var storage = dbStorage.First(p => p.partId == partId);
                var projects = dbProjects.First(p => p.ProjectId == projectId);

                if(reserve is null) {
                    throw new Exception($"Reserve with Id '{reservedId}' not found.");
                }

                if(reserve.neededAmount <= storage.countOfParts) {
                    storage.countOfParts -= reserve.neededAmount;

                    _context.Remove(_mapper.Map<reservedParts>(reserve));
                }
                else {

                    var updated = new UpdateReserveDto() 
                    {
                        reservedPartsId = reservedId,
                        neededAmount = Math.Abs(storage.countOfParts-reserve.neededAmount)
                    };

                    await updateReserve(updated);

                    storage.countOfParts = 0;
                }

            }
            catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }
    }
}