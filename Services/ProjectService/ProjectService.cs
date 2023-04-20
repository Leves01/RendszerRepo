using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
         private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProjectService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        //projectid, partid, quantity, combinedPrice
        private static List<Part> projects = new List<Part> {};

        public async Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price)
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            var dbProjects = await _context.Project.ToListAsync();

            try{
                var selectedProject = dbProjects.FirstOrDefault(u => (u.ProjectId == projektid));
                if(selectedProject is null) {
                    throw new Exception($"Project with Id '{projektid}' not found.");
                }
                selectedProject.workPrice = price;
                selectedProject.workTime = time;
                serviceResponse.Data = _mapper.Map<GetProjectDto>(selectedProject);
                
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
                
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
    }
}