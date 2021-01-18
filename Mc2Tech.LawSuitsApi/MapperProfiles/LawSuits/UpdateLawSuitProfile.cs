using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class UpdateLawSuitProfile : Profile
    {
        public UpdateLawSuitProfile()
        {
            CreateMap<UpdateLawSuitModel, UpdatedLawSuitEvent>();

            CreateMap<UpdateLawSuitModel, LawSuitEntity>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => destMember == null));
        }
    }
}
