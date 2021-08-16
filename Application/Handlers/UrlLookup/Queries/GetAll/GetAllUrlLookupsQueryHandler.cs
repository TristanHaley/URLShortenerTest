using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Queries.Shared;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.UrlLookup.Queries.GetAll
{
    public class GetAllUrlLookupsQueryHandler : IRequestHandler<GetAllUrlLookupsQuery, UrlLookupListViewModel>
    {
        private readonly IUrlShortenerContext _context;
        private readonly IMapper              _mapper;

        public GetAllUrlLookupsQueryHandler(IUrlShortenerContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }
        
        public async Task<UrlLookupListViewModel> Handle(GetAllUrlLookupsQuery request, CancellationToken cancellationToken)
        {
            return new UrlLookupListViewModel
            {
                Lookups = await _context.UrlLookups
                                        .AsNoTracking()
                                        .OrderBy(urlLookup => urlLookup.Url)
                                        .ProjectTo<UrlLookupModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken)
            };
        }
    }
}