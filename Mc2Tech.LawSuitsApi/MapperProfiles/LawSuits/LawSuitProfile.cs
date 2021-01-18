using AutoMapper;
using Mc2Tech.Crosscutting.ViewModel.LawSuits;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.Model.LawSuits;

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
