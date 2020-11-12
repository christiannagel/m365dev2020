using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebAppForAzureAppConfig.Pages
{
    public class SecretPageModel : PageModel
    {
        public string Secret1 { get; }
        public SecretPageModel(IConfiguration configuration)
        {
            Secret1 = configuration["TheSecret1"];
        }

        public void OnGet()
        {
        }
    }
}
