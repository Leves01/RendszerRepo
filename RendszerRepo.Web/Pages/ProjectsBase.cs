using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Project;
using RendszerRepo.Models;
using RendszerRepo.Web.Services;
using RendszerRepo.Web.Services.Contracts;

namespace RendszerRepo.Web.Pages
{
    public class ProjectsBase : ComponentBase
    {
        [Inject]
        public FEIProjectService FEProjectService { get; set; }

        public ServiceResponse<List<GetPrDto>> Projects { get; set; }
        protected bool ShowSuccessMessage { get; set; } = false;
        protected bool ShowErrorMessage { get; set; } = false;
        protected string SuccessMessage { get; set; }
        protected string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Projects = await FEProjectService.GetProjects();
        }
    }
}
