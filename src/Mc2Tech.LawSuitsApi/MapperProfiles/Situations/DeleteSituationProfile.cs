using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.Situations
{
    public class DeleteSituationProfile : Profile
    {
        public DeleteSituationProfile()
        {
            CreateMap<SituationEntity, DeletedSituationEvent>();
            CreateMap<DeleteSituationModel, SituationEntity>();
        }
    }
}
