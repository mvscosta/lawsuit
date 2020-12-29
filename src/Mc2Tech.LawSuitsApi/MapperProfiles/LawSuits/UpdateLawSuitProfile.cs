using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class UpdateLawSuitProfile : Profile
    {
        public UpdateLawSuitProfile()
        {
            CreateMap<UpdateLawSuitCommand, LawSuitEntity>();
            CreateMap<LawSuitEntity, UpdatedLawSuitEvent>();
            CreateMap<UpdateLawSuitModel, LawSuitEntity>();
        }
    }
}
