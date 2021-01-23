using AutoMapper;
using Mc2Tech.Crosscutting.Interfaces.Persons;
using Mc2Tech.PersonsApi.Model;

namespace Mc2Tech.PersonsApi.MapperProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonEntity, Person>().ReverseMap();
            CreateMap<PersonEntity, PersonBasicInformation>().ReverseMap();
            CreateMap<PersonBasicInformation, IPersonDto>().ReverseMap();
            CreateMap<Person, IPersonDto>().ReverseMap();
        }
    }
}
