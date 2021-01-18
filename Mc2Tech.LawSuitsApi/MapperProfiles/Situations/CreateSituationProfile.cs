using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.Situations
{
    public class CreateSituationProfile : Profile
    {
        public CreateSituationProfile()
        {
            CreateMap<CreateSituationModel, SituationEntity>();
            CreateMap<SituationEntity, CreatedSituationEvent>();
        }
    }
}
