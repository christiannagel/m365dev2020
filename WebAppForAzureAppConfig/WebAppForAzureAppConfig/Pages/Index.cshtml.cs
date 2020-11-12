using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebAppForAzureAppConfig.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IOptionsSnapshot<IndexAppSettings> options, ILogger<IndexModel> logger)
        {
            _logger = logger;
            Config1 = options.Value.Config1 ?? "no value";
        }

        public string Config1 { get; }

        public void OnGet()
        {

        }
    }
}
