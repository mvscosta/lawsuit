using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.Situations;
using Mc2Tech.LawSuitsApi.ViewModel.Situations;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.Situations
{
    public class SituationProfile : Profile
    {
        public SituationProfile()
        {
            CreateMap<SituationEntity, Situation>().ReverseMap();

            CreateMap<Situation, SituationModel>().ReverseMap();
        }
    }
}
