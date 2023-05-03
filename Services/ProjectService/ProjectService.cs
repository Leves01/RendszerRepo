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
            public ProjectService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
         
     public async Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject)
        {
            var serviceResponse = new ServiceResponse<List<GetPrDto>>();
            var dbProject = await _context.Project.ToListAsync();

            var addedProject = dbProject.FirstOrDefault(u => (u.ProjectName == newProject.ProjectName));

            if(addedProject is not null) {
                throw new Exception($"'{newProject.ProjectName}' project already exists.");
            }
            else {
                _context.Project.Add(_mapper.Map<Project>(newProject));
            }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
            
        }

        private static List<Project> ProjectdetailList = new List<Project> {};
        public async Task<ServiceResponse<List<GetPrDto>>> GetProjects()
        {
            var serviceResponse = new ServiceResponse<List<GetPrDto>>();
            var dbProject = await _context.Project.ToListAsync();
            serviceResponse.Data = dbProject.Select(s => _mapper.Map<GetPrDto>(s)).ToList();
            return serviceResponse;
        }

        //projectid, partid, quantity, combinedPrice
        private static List<Part> projects = new List<Part> {};
     
        public async Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price) {

            var serviceResponse = new ServiceResponse<GetProjectDto>();
            var dbProjects = await _context.Project.ToListAsync();

            try{
                var selectedProject = dbProjects.FirstOrDefault(u => (u.ProjectId == projektid));
                    throw new Exception($"Project with Id '{projektid}' not found.");
                if(selectedProject is null) {
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
