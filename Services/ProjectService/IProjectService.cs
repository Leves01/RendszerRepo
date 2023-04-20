using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.ProjectService
{
    public interface IProjectService
    {
       // Task<ServiceResponse<List<GetStoragesDto>>> GetStorages();
       Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price);
    }
}