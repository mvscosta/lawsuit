using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class CreateLawSuitProfile : Profile
    {
        public CreateLawSuitProfile()
        {
            CreateMap<LawSuitEntity, CreateLawSuitModel>();
            CreateMap<CreateLawSuitModel, LawSuitEntity>();
            CreateMap<LawSuitEntity, CreatedLawSuitEvent>();
        }
    }
}
