using AutoMapper;
using Mc2Tech.Crosscutting.Interfaces.LawSuits;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel;

namespace Mc2Tech.PersonsApi.MapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonEntity, Person>().ReverseMap();

            CreateMap<Person, IPersonDto>().ReverseMap();
        }
    }
}
