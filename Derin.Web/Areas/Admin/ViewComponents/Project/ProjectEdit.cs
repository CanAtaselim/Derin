using AutoMapper;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Common;
using Derin.Web.WebCommon;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Project
{
    public class ProjectEdit : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IMapper _mapper;
        public ProjectEdit(AdministrationBLLocator administrationBLLocator, IMapper mapper)
        {
            _administrationBLLocator = administrationBLLocator;
            _mapper = mapper;
        }


        public Task<IViewComponentResult> InvokeAsync(long IdProject)
        {

            ProjectVM Project = _mapper.Map<ProjectVM>(_administrationBLLocator.ProjectBL.CRUD.GetById(IdProject));


            return Task.FromResult<IViewComponentResult>(View(Project ?? new ProjectVM()));
        }

    }
}
