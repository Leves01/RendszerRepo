using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.PartService
{
    public interface IPartService
    {
        Task<ServiceResponse<List<GetPartDto>>> GetAllParts();
        Task<ServiceResponse<List<GetPartDto>>> AddPart(AddPartDto newPart);
        Task<ServiceResponse<GetPartDto>> UpdatePart(UpdatePartDto updatedPart);
        Task<ServiceResponse<List<GetPartDto>>> DeletePart(int id);
        Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart);
        Task<ServiceResponse<GetPartDto>> PartToProject(int selectedPartId, int selectedProjectId, int quantity);
        
    }
}