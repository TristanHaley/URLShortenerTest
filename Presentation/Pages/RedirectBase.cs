using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using UrlShortenerApi;

namespace Presentation.Pages
{
    public class RedirectBase: ComponentBase
    {
        [Parameter] public string Key { get; set; }
        
        [Inject] private ILogger<RedirectBase> Logger            { get; set; }
        [Inject] private IUrlLookupClient      UrlLookupClient   { get; set; }
        [Inject] private NavigationManager     NavigationManager { get; set; }

        protected bool RedirectNotFound { get; set; } = true;
        
        protected override async Task OnInitializedAsync()
        {
            // Guard: Invalid Url            
            if (string.IsNullOrWhiteSpace(Key))
            {
                RedirectNotFound = true;
                return;
            }

            var response = await UrlLookupClient.ByKeyAsync(Key);
            if (response.StatusCode == 200) NavigationManager.NavigateTo(response.Result.Url);
        }
    }
}