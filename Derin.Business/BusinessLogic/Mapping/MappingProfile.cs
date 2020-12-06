
using AutoMapper;
using Derin.Business.ViewModel.Administration;
using Derin.Data.Model;

namespace Derin.Business.BusinessLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonVM>();
            CreateMap<Services, ServicesVM>();
            CreateMap<Project, ProjectVM>();
        }

    }
}
