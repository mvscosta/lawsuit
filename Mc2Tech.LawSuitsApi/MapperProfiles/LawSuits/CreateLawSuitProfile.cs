using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class CreateLawSuitProfile : Profile
    {
        public CreateLawSuitProfile()
        {
            CreateMap<CreateLawSuitModel, LawSuitEntity>()
                .ForMember(p => p.LawSuitResponsibles, o => o.Ignore())
                .ReverseMap();
            CreateMap<LawSuitEntity, CreatedLawSuitEvent>()
                .ForMember(p => p.LawSuitId, o => o.MapFrom(p => p.Id));
        }
    }
}
