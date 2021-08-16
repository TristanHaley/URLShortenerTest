using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Presentation.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        #region Constructors

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        #endregion

        private readonly ILogger<ErrorModel> _logger;

        public bool   ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string RequestId     { get; set; }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}