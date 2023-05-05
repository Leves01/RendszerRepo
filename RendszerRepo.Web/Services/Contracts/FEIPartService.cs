using RendszerRepo.Dtos.Part;
using RendszerRepo.Models;

namespace RendszerRepo.Web.Services.Contracts
{
	public interface FEIPartService
	{
		Task<ServiceResponse<List<GetPartDto>>> GetAllParts();
		Task<ServiceResponse<List<GetPartDto>>> AddPart(AddPartDto newPart);
		Task<ServiceResponse<GetPartDto>> UpdatePart(UpdatePartDto updatedPart);
		Task<ServiceResponse<List<GetPartDto>>> DeletePart(int id);
		Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart);
	}
}
