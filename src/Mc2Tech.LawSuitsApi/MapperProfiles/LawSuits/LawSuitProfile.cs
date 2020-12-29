using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.LawSuits;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class LawSuitProfile : Profile
    {
        public LawSuitProfile()
        {
            CreateMap<LawSuitEntity, LawSuit>().ReverseMap();

            CreateMap<LawSuit, LawSuitModel>().ReverseMap();
        }
    }
}
