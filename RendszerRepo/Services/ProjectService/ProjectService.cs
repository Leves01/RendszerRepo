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

        public async Task<ServiceResponse<List<GetProject_propertiesDto>>> getProjectProperties()
        {
            var serviceResponse = new ServiceResponse<List<GetProject_propertiesDto>>();
            var dbProjectProperties = await _context.ProjectProperties.ToListAsync();
            serviceResponse.Data = dbProjectProperties.Select(s => _mapper.Map<GetProject_propertiesDto>(s)).ToList();
            return serviceResponse;
        }

        //projectid, partid, quantity, combinedPrice
        private static List<Part> projects = new List<Part> {};
     
        public async Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price) 
        {
            var serviceResponse = new ServiceResponse<GetProjectDto>();
            var dbProjectProperties = await _context.ProjectProperties.ToListAsync();
            var dbProject = await _context.Project.ToListAsync();

            try{
                var selectedProject = dbProjectProperties.FirstOrDefault(u => (u.ProjectId == projektid));
                var project = dbProject.First(u => (u.ProjectId == projektid));

                if(selectedProject is null)
                {
                    throw new Exception($"Project with Id '{projektid}' not found.");
                }

                selectedProject.workPrice = price;
                selectedProject.workTime = time;
                project.Status = "Wait";

                serviceResponse.Data = _mapper.Map<GetProjectDto>(selectedProject);
            } catch(Exception ex) {
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
                
            await _context.SaveChangesAsync();
            return serviceResponse;   
        }

        // public async Task<ServiceResponse<GetProjectDto>> setStatusInProgress(UpdateStatusDto updated) 
        // {
        //     var serviceResponse = new ServiceResponse<GetProjectDto>();
        //     var dbProjects = await _context.Project.ToListAsync();
        //     var dbReserved = await _context.Reserves.ToListAsync();

        //     try {

        //         var project = dbProjects.FirstOrDefault(u => (u.ProjectId == updated.projectId));
        //         var selectedReserved = dbReserved.FirstOrDefault(u => (u.projectId == updated.projectId));

        //         if(selectedReserved is null)
        //         {
        //             project.Status = "InProgress";
        //         }
                
        //     } catch(Exception ex) {
        //         serviceResponse.Success = false;
        //         serviceResponse.Message = ex.Message;
        //     }

        //     await _context.SaveChangesAsync();
        //     return serviceResponse;
        // }

       public async Task<ServiceResponse<GetProject_propertiesDto>> PriceCalculation(int projektid) 
        {
            var serviceResponse = new ServiceResponse<GetProject_propertiesDto>();
            var dbProjects = await _context.Project.ToListAsync();
            var dbParts = await _context.Parts.ToListAsync();
            var dbProjecProperties = await _context.ProjectProperties.ToListAsync();
            var dbReserved = await _context.Reserves.ToListAsync();
            

            try{
                var selectedProject = dbProjects.FirstOrDefault(u => (u.ProjectId == projektid));
                var selectedPP = dbProjecProperties.FirstOrDefault(u => (u.ProjectId == projektid));
                var selectedPart = dbParts.FirstOrDefault(u => (u.partId == selectedPP.partId));
                var selectedReserved = dbReserved.FirstOrDefault(u => (u.projectId==projektid));

                if(selectedReserved is null)
                {
                    selectedPP.combinedPrice = (selectedPP.workPrice * selectedPP.workTime) + (selectedPart.price * selectedPP.quantity);
                    selectedProject.Status = "InProgress";
                }
                else
                {
                    selectedPP.combinedPrice = (selectedPP.workPrice * selectedPP.workTime) + (selectedPart.price * selectedPP.quantity);
                    selectedProject.Status = "Scheduled";
                }
                
                serviceResponse.Data = _mapper.Map<GetProject_propertiesDto>(selectedPP);

            } catch(Exception ex) {
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPrDto>> ProjectStatusChange(UpdateStatusDto newStatus) 
        {
            var serviceResponse = new ServiceResponse<GetPrDto>();
            var dbProjects = await _context.Project.ToListAsync();

            try{
                var selectedProjectProperties = dbProjects.FirstOrDefault(u => (u.ProjectId == newStatus.projectId));
                if(selectedProjectProperties  is null)
                {
                    throw new Exception($"Project with Id '{newStatus.projectId}' not found.");
                }
                
                selectedProjectProperties.Status = newStatus.Status;
                serviceResponse.Data = _mapper.Map<GetPrDto>(selectedProjectProperties);
            } catch(Exception ex) {
                
                
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            await _context.SaveChangesAsync();
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
