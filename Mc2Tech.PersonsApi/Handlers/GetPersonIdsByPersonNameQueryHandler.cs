using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mc2Tech.PersonsApi.DAL;
using Mc2Tech.PersonsApi.Model;
using Mc2Tech.PersonsApi.ViewModel.Get;
using Microsoft.EntityFrameworkCore;
using SimpleSoft.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2Tech.PersonsApi.Handlers
{
    public class GetPersonIdsByPersonNameQueryHandler : IQueryHandler<GetPersonIdsByPersonNameQuery, List<Guid>>
    {
        private readonly IQueryable<PersonEntity> _persons;
        private readonly IMapper _mapper;

        public GetPersonIdsByPersonNameQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _persons = context.Set<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<List<Guid>> HandleAsync(GetPersonIdsByPersonNameQuery query, CancellationToken ct)
        {
            var filter = _persons.Where(p => p.Name.Contains(query.PersonName)).Select(p => p.Id.Value);

            var result = await filter
                .ToListAsync(ct);

            return result;
        }
    }
}
