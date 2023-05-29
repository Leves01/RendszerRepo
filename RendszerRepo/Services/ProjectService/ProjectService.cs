using RendszerRepo.Models.Dtos.Project;
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
     
        public async Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price) 
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            var dbProjectProperties = await _context.ProjectProperties.ToListAsync();

            try{
                var selectedProject = dbProjectProperties.FirstOrDefault(u => (u.ProjectId == projektid));
                if(selectedProject is null)
                {
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

       /*public async Task<ServiceResponse<GetProjectDto>> PriceCalculation(int projektid) 
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            var dbProjects = await _context.Project.ToListAsync();
            var dbParts = await _context.Parts.ToListAsync();
            
            try{
                var selectedProject = dbProjects.FirstOrDefault(u => (u.ProjectId == projektid));
                if(selectedProject is null)
                {
                    throw new Exception($"Project with Id '{projektid}' not found.");
                }
                
                
                serviceResponse.Data = _mapper.Map<GetProjectDto>(selectedProject);
            } catch(Exception ex) {
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }*/

        public async Task<ServiceResponse<GetProject_propertiesDto>> ProjectStatusChange(UpdateStatusDto newStatus) 
        {
            var serviceResponse = new ServiceResponse<GetProject_propertiesDto>();
            var dbProjects = await _context.Project.ToListAsync();
            try{
                var selectedProjectProperties = dbProjects .FirstOrDefault(u => (u.ProjectId == newStatus.projectId));
                if(selectedProjectProperties  is null)
                {
                    throw new Exception($"Project with Id '{newStatus.projectId}' not found.");
                }
                
                selectedProjectProperties.Status = newStatus.Status;
                serviceResponse.Data = _mapper.Map<GetProject_propertiesDto>(selectedProjectProperties);
            } catch(Exception ex) {
                
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetProject_propertiesDto>>> AddProject_properties(AddProjectDto newProjectPr)
        {
            var serviceResponse = new ServiceResponse<List<GetProject_propertiesDto>>();
            var dbProject = await _context.Project.ToListAsync();

            var addedProject = dbProject.FirstOrDefault(u => (u.ProjectId == newProjectPr.ProjectId));

            if(addedProject is not null) {
                _context.ProjectProperties.Add(_mapper.Map<Project_properties>(newProjectPr));
                
            }
            else {
                throw new Exception($"'{newProjectPr.ProjectId}' does not exists");
            }
            
            await _context.SaveChangesAsync();
            return serviceResponse;
            
        }
    }
}
