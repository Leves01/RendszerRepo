using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.PartService
{
    public interface IPartService
    {
        Task<ServiceResponse<List<Part>>> GetAllParts();
        Task<ServiceResponse<List<Part>>> AddPart(Part newPart);
        Task<ServiceResponse<Part>> UpdatePart(Part updatedPart);
        Task<ServiceResponse<List<Part>>> DeletePart(int id);
        
    }
}