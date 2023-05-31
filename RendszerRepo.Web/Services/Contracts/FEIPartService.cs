using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Reserves;

namespace RendszerRepo.Web.Services.Contracts
{
	public interface FEIPartService
	{
		Task<ServiceResponse<List<GetPartDto>>> GetAllParts();
		Task<ServiceResponse<List<GetPartDto>>> AddPart(AddPartDto newPart);
		Task<ServiceResponse<GetPartDto>> UpdatePart(UpdatePartDto updatedPart);
		Task<ServiceResponse<List<GetPartDto>>> DeletePart(int id);
		Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart);
        Task<ServiceResponse<GetPartDto>> PartToProject(PartToProjectDto newdto);
        Task<ServiceResponse<List<GetReserveDto>>> getReserves();
        Task<ServiceResponse<List<GetReserveDto>>> addReserves(AddReserveDto newReserve);
        Task<ServiceResponse<GetReserveDto>> updateReserve(UpdateReserveDto updateReserve);
        Task<ServiceResponse<GetStoragesDto>> fillReserves(FillReservesDto fillReservesDto);
    }
}
