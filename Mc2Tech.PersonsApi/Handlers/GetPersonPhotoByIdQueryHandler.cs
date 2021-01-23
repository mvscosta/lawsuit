using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Get;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class GetPersonPhotoByIdQueryHandler : IQueryHandler<GetPersonPhotoByIdQuery, byte[]>
    {
        private readonly IQueryable<PersonEntity> _persons;

        public GetPersonPhotoByIdQueryHandler(ApiDbContext context)
        {
            _persons = context.Set<PersonEntity>();
        }

        public async Task<byte[]> HandleAsync(GetPersonPhotoByIdQuery query, CancellationToken ct)
        {
            var filter = _persons
                .Where(p => p.Id == query.PersonId)
                .Select(p => p.Photo);


            var result = await filter
                .FirstOrDefaultAsync(ct);

            return result;
        }
    }
}
