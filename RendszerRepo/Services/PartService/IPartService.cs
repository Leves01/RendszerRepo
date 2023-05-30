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
        Task<ServiceResponse<GetPartDto>> DeletePart(int id);
        Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart);
        Task<ServiceResponse<GetPartDto>> PartToProject(PartToProjectDto newdto);
        Task<ServiceResponse<List<GetReserveDto>>> getReserves();
        Task<ServiceResponse<List<GetReserveDto>>> addReserves(AddReserveDto newReserve);       
        Task<ServiceResponse<GetReserveDto>> updateReserve(UpdateReserveDto updateReserve);
        Task<ServiceResponse<GetStoragesDto>> fillReserves(int reservedId, int partId, int projectId);
    }
}