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

        public async Task<ServiceResponse<List<GetPartDto>>> DeletePart(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPartDto>>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.First(p => p.partId == id);
                if(part is null) {
                    throw new Exception($"Part with Id '{id}' not found.");
                }

                dbParts.Remove(part);
            
                serviceResponse.Data = dbParts.Select(p => _mapper.Map<GetPartDto>(p)).ToList();
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPartDto>> PartToProject(int selectedPartId, int selectedProjectId, int selectedQuantity)
        {
            var serviceResponse = new ServiceResponse<GetPartDto>();
            var dbParts = await _context.Parts.ToListAsync();
            var dbStorage = await _context.Storages.ToListAsync();
            var dbPrProp = await _context.ProjectProperties.ToListAsync();

            try {
                var part = dbParts.First(p => p.partId == selectedPartId);
                var project = dbPrProp.First(u => (u.ProjectId == selectedProjectId));
                var storage = dbStorage.First(p => p.partId == selectedPartId);

                if(part is null) {
                    throw new Exception($"Part with Id '{selectedPartId}' not found.");
                }
                if(project is null) {
                    throw new Exception($"Project with Id '{selectedProjectId}' not found.");
                }
                
                project.partId = part.partId;
                project.quantity = selectedQuantity;
                if((storage.countOfParts-selectedQuantity)<0)
                    throw new Exception($"Not enough parts.");
                else
                    storage.countOfParts-=selectedQuantity;

                
                serviceResponse.Data = _mapper.Map<GetPartDto>(project);
                serviceResponse.Data = _mapper.Map<GetPartDto>(storage);
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