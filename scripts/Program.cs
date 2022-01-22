using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddHostedService<GeneratePlantuml>();
                services.AddSingleton<IImageManager, SvgManager>();
            })
            .Build()
            .RunAsync();
    }    
}