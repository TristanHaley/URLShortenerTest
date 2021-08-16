using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Serilog;
using UrlShortenerApi;

namespace Presentation.Components.UrlListings
{
    public class UrlListingsViewBase : ComponentBase
    {
        [Inject] private IUrlLookupClient             UrlLookupClient { get; set; }
        [Inject] private ILogger<UrlListingsViewBase> Logger          { get; set; }
        
        protected UrlLookupListViewModel UrlLookupModel { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            UrlLookupModel = new UrlLookupListViewModel
            {
                Lookups = new List<UrlLookupModel>()
            };
            
            var request = await UrlLookupClient.GetAllAsync();

            if (request.StatusCode != 200)
            {
                Logger.LogWarning("Failed to fetch URL lookups: \"{Code}\"", request.StatusCode);
                return;
            }

            UrlLookupModel = request.Result;
        }
    }
}