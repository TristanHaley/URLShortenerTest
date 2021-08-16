using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Application.Handlers.UrlLookup.Queries.Shared;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UrlLookup.Queries.GetByUrl
{
    public class GetUrlLookupByUrlQueryHandler : IRequestHandler<GetUrlLookupByUrlQuery, UrlLookupModel>
    {
        private readonly IUrlShortenerContext _context;
        private readonly IMapper              _mapper;

        public GetUrlLookupByUrlQueryHandler(IUrlShortenerContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }
        
        public async Task<UrlLookupModel> Handle(GetUrlLookupByUrlQuery request, CancellationToken cancellationToken)
        {
            var decodedUrl = HttpUtility.UrlDecode(request.Url);
            
            return await _context.UrlLookups
                                 .AsNoTracking()
                                 .Where(urlLookup => urlLookup.Url == decodedUrl)
                                 .ProjectTo<UrlLookupModel>(_mapper.ConfigurationProvider)
                                 .SingleOrDefaultAsync(cancellationToken);
        }
    }
}