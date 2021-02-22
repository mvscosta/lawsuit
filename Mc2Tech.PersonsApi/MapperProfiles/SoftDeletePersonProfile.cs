using AutoMapper;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Delete;

namespace Mc2Tech.PersonsApi.MapperProfiles
{
    public class SoftDeletePersonProfile : Profile
    {
        public SoftDeletePersonProfile()
        {
            CreateMap<PersonEntity, SoftDeletedPersonEvent>();
            CreateMap<SoftDeletePersonModel, PersonEntity>();
        }
    }
}
