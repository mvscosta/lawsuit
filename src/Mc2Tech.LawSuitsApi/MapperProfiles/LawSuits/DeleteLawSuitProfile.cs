using AutoMapper;
using Mc2Tech.LawSuitsApi.Model.DALEntity;
using Mc2Tech.LawSuitsApi.ViewModel.LawSuits;

namespace Mc2Tech.LawSuitsApi.MapperProfiles.LawSuits
{
    public class DeleteLawSuitProfile : Profile
    {
        public DeleteLawSuitProfile()
        {
            CreateMap<LawSuitEntity, DeletedLawSuitEvent>();
            CreateMap<DeleteLawSuitModel, LawSuitEntity>();
        }
    }
}
