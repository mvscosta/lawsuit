using AutoMapper;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Update;

namespace Mc2Tech.PersonsApi.MapperProfiles
{
    public class UpdatePersonProfile : Profile
    {
        public UpdatePersonProfile()
        {
            CreateMap<UpdatePersonCommand, PersonEntity>();
            CreateMap<PersonEntity, UpdatedPersonEvent>();
            CreateMap<UpdatePersonModel, PersonEntity>();
        }
    }
}
