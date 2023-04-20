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
         
     public async Task<ServiceResponse<List<GetProject_propertiesDto>>> AddProject(AddProjectDto newProject)
        {
            var serviceResponse = new ServiceResponse<List<GetProject_propertiesDto>>();
            var dbProject = await _context.ProjectProperties.ToListAsync();

            var addedProject = dbProject.FirstOrDefault(u => (u.ProjectName == newProject.ProjectName));

            if(addedProject is not null) {
                throw new Exception($"'{newProject.ProjectName}' part already exists.");
            }
            else {
                _context.ProjectProperties.Add(_mapper.Map<Project_properties>(newProject));
            }
             async Task<ServiceResponse<List<GetPrDto>>> AddPr(AddPrDto newprj)
        {
            var serviceResponse = new ServiceResponse<List<GetPrDto>>();
            var dbProject = await _context.Project.ToListAsync();

            var addedProject = dbProject.FirstOrDefault(u => (u.ProjectName == newprj.ProjectName));

            if(addedProject is not null) {
                throw new Exception($"'{newprj.ProjectName}' project already exists.");
            }
            else {
                _context.Project.Add(_mapper.Map<Project>(newprj));
            }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
        }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
            
        }

        private static List<Project_properties> ProjectdetailList = new List<Project_properties> {};
        public async Task<ServiceResponse<List<GetProject_propertiesDto>>> GetProjects()
        {
            var serviceResponse = new ServiceResponse<List<GetProject_propertiesDto>>();
            var dbProject_properties = await _context.ProjectProperties.ToListAsync();
            serviceResponse.Data = dbProject_properties.Select(s => _mapper.Map<GetProject_propertiesDto>(s)).ToList();
            return serviceResponse;
        }
    }
}