using AutoMapper;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Delete;

namespace Mc2Tech.MapperProfiles.LawSuits
{
    public class DeletePersonProfile : Profile
    {
        public DeletePersonProfile()
        {
            CreateMap<PersonEntity, DeletedPersonEvent>();
            CreateMap<DeletePersonModel, PersonEntity>();
        }
    }
}
