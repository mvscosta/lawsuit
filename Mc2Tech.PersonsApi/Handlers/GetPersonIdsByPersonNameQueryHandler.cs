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
        private readonly IQueryable<PersonEntity> _lawSuits;
        private readonly IMapper _mapper;

        public GetPersonIdsByPersonNameQueryHandler(ApiDbContext context, IMapper mapper)
        {
            _lawSuits = context.Set<PersonEntity>();
            _mapper = mapper;
        }

        public async Task<List<Guid>> HandleAsync(GetPersonIdsByPersonNameQuery query, CancellationToken ct)
        {
            var filter = _lawSuits.Where(p => p.Name.Contains(query.PersonName)).Select(p => p.Id.Value);

            var result = await filter
                //.ProjectTo<Guid>(configuration: _mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return result;
        }
    }
}
