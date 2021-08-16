using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using UrlShortenerApi;

namespace Presentation.Pages
{
    public class IndexBase : ComponentBase
    {
        public IndexBase()
        {
            CreateModel = new CreateUrlLookupCommand();
        }
        
        [Inject] private IUrlLookupClient   UrlLookupClient   { get; set; }
        [Inject] private ILogger<IndexBase> Logger            { get; set; }
        [Inject] private NavigationManager  NavigationManager { get; set; }
        
        protected CreateUrlLookupCommand CreateModel { get;  }

        protected async Task HandleFormSubmissionAsync()
        {
            // Guard: Invalid URL
            if (string.IsNullOrWhiteSpace(CreateModel.Url)) return;

            var response = await UrlLookupClient.CreateAsync(CreateModel);
            if (response.StatusCode == 204)
            {
                HasShortenedUrlToShow = true;

                var request = await UrlLookupClient.ByUrlAsync(CreateModel.Url);
                ShortenedUrl = $"{NavigationManager.BaseUri}{request.Result.Key}";
            }
            else
            {
                HasShortenedUrlToShow = false;
            }
        }
        
        protected bool   HasShortenedUrlToShow { get; set; }
        protected string ShortenedUrl           { get; set; }
    }
}