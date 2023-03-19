using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.PartService
{
    public class PartService : IPartService
    {
        private readonly DataContext _context;
        public PartService(DataContext context)
        {
            _context = context;
        }
        //partId, partName, price, maxCount
        private static List<Part> parts = new List<Part> {};

        public async Task<ServiceResponse<List<Part>>> GetAllParts()
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            var dbParts = await _context.Parts.ToListAsync();
            serviceResponse.Data = dbParts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Part>>> AddPart(Part newPart)
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            var dbParts = await _context.Parts.ToListAsync();

            var addedPart = dbParts.FirstOrDefault(u => u.partName == newPart.partName);

            if(addedPart is not null) {
                throw new Exception($"'{newPart.partName}' part already exists.");
            }
            else {
                _context.Parts.Add(newPart);
            }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<Part>> UpdatePart(Part updatedPart)
        {
            var serviceResponse = new ServiceResponse<Part>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.FirstOrDefault(p => p.partId == updatedPart.partId);
                if(part is null) {
                    throw new Exception($"Part with Id '{updatedPart.partId}' not found.");
                }

                part.partName = updatedPart.partName;
                part.price = updatedPart.price;
                part.maxCount = updatedPart.maxCount;
            
                serviceResponse.Data = part;
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Part>>> DeletePart(int id)
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            var dbParts = await _context.Parts.ToListAsync();

            try {
                var part = dbParts.FirstOrDefault(p => p.partId == id);
                if(part is null) {
                    throw new Exception($"Part with Id '{id}' not found.");
                }

                _context.Parts.Remove(part);
            
                serviceResponse.Data = parts;
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            await _context.SaveChangesAsync();
            
            return serviceResponse;
        }
    }
}