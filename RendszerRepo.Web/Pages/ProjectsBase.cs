using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Project;
using RendszerRepo.Dtos.Project_properties;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Project;
using RendszerRepo.Web.Services;
using RendszerRepo.Web.Services.Contracts;

namespace RendszerRepo.Web.Pages
{
    public class ProjectsBase : ComponentBase
    {
        [Inject]
        public FEIProjectService FEProjectService { get; set; }

        public ServiceResponse<List<GetPrDto>> Projects { get; set; }
        protected AddPrDto NewProject { get; set; } = new AddPrDto();
        protected bool ShowSuccessMessage { get; set; } = false;
        protected bool ShowErrorMessage { get; set; } = false;
        protected string SuccessMessage { get; set; }
        protected string ErrorMessage { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async Task AddNewProject()
        {
            var response = await FEProjectService.AddProject(NewProject);

            if (response.Success)
            {
                NavigationManager.NavigateTo("/projects");
                ShowSuccessMessage = true;
                SuccessMessage = "Project added successfully!";
                NewProject = new AddPrDto();

            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }
        protected async Task UpdateStatus(GetPrDto project)
        {
            var response = await FEProjectService.ProjectStatusChange(new UpdateStatusDto
            {
                projectId = project.projectId,
                Status = project.Status
            });

            if (response.Success)
            {
                ShowSuccessMessage = true;
                SuccessMessage = "Project updated successfully!";
                NavigationManager.NavigateTo("/projects");
            }
            else
            {
                ShowErrorMessage = true;
                ErrorMessage = response.Message;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            Projects = await FEProjectService.GetProjects();
        }
    }
}
