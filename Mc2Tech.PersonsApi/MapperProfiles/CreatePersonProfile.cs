using AutoMapper;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Create;

namespace Mc2Tech.PersonsApi.MapperProfiles
{
    public class CreatePersonProfile : Profile
    {
        public CreatePersonProfile()
        {
            CreateMap<CreatePersonModel, PersonEntity>();
            CreateMap<PersonEntity, CreatePersonModel>();
            CreateMap<PersonEntity, CreatedPersonEvent>();
        }
    }
}
