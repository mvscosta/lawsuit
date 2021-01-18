using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.Situations
{
    public class UpdateSituationProfile : Profile
    {
        public UpdateSituationProfile()
        {
            CreateMap<UpdateSituationCommand, SituationEntity>();
            CreateMap<SituationEntity, UpdatedSituationEvent>();
            CreateMap<UpdateSituationModel, SituationEntity>();
        }
    }
}
