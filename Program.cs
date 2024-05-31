using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Top10Streams.Data;
using MudBlazor.Services;
using CurrieTechnologies.Razor.Clipboard;

namespace Top10Streams
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ListService>();

            builder.Services.AddMudServices();
            builder.Services.AddClipboard();

            await builder.Build().RunAsync();
        }
    }
}
