using AutoMapper;
using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Derin.Web.Areas.Admin.ViewComponents.Talent
{
    public class TalentEdit : ViewComponent
    {
        private AdministrationBLLocator _administrationBLLocator;
        private IMemoryCache _memoryCache;
        private IMapper _mapper;
        public TalentEdit(AdministrationBLLocator administrationBLLocator, IMemoryCache memoryCache, IMapper mapper)
        {
            _administrationBLLocator = administrationBLLocator;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        //public Task<IViewComponentResult> InvokeAsync(long IdTalent)
        //{

        //    TalentVM talent = _mapper.Map<TalentVM>(_administrationBLLocator.TalentBL.CRUD.GetById(IdTalent));
            

        //    return Task.FromResult<IViewComponentResult>(View(talent ?? new TalentVM()));
        //}

    }
}
