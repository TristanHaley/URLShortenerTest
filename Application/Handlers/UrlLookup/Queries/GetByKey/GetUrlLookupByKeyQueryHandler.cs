using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Queries.Shared;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UrlLookup.Queries.GetByKey
{
    public class GetUrlLookupByKeyQueryHandler : IRequestHandler<GetUrlLookupByKeyQuery, UrlLookupModel>
    {
        private readonly IUrlShortenerContext _context;
        private readonly IMapper              _mapper;

        public GetUrlLookupByKeyQueryHandler(IUrlShortenerContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }
        
        public async Task<UrlLookupModel> Handle(GetUrlLookupByKeyQuery request, CancellationToken cancellationToken)
        {
            return await _context.UrlLookups
                                 .AsNoTracking()
                                 .Where(urlLookup => urlLookup.Key == request.Key)
                                 .ProjectTo<UrlLookupModel>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(cancellationToken);
        }
    }
}