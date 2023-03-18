using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.PartService
{
    public class PartService : IPartService
    {
        //partId, partName, price, maxCount
        private static List<Part> parts = new List<Part> {
            new Part(),
            new Part{partName = "panel", price = 5000, maxCount = 3},
            new Part{partName = "inverter", price = 100, maxCount = 100}
        };

        public async Task<ServiceResponse<List<Part>>> GetAllParts()
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            serviceResponse.Data = parts;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Part>>> AddPart(Part newPart)
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            parts.Add(newPart);
            serviceResponse.Data = parts;
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<Part>> UpdatePart(Part updatedPart)
        {
            var serviceResponse = new ServiceResponse<Part>();
            try {
                var part = parts.FirstOrDefault(p => p.partId == updatedPart.partId);
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
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Part>>> DeletePart(int id)
        {
            var serviceResponse = new ServiceResponse<List<Part>>();
            try {
                var part = parts.FirstOrDefault(p => p.partId == id);
                if(part is null) {
                    throw new Exception($"Part with Id '{id}' not found.");
                }

                parts.Remove(part);
            
                serviceResponse.Data = parts;
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
    }
}