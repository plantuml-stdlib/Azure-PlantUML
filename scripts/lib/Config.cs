using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

 
public class ConfigLookupEntry
{
    public string? Category { get; set; }
    public string? ServiceSource { get; set; }
    public string? ServiceTarget { get; set; }
    public bool FitToCanvas { get; set; }
}

public class Config
{
    public List<AzureCategory>? Categories { get; set; }

    public static IEnumerable<ConfigLookupEntry> ReadConfig(string configFilePath)
    {
        Config config;
        using (var input = File.OpenText("Config.yaml"))
        {
            var deserializer = new DeserializerBuilder()
                .Build();

            config = deserializer.Deserialize<Config>(input);
        }

        var lookupTable = config.Categories!.SelectMany(cat => cat.Services!.Select(service => new ConfigLookupEntry
        {
            Category = cat.Name,
            ServiceSource = service.Source,
            ServiceTarget = service.Target,
            FitToCanvas = service.Fit
        }));

        return lookupTable;
    }
}

public class AzureCategory
{
    public string? Name { get; set; }

    public List<AzureService>? Services { get; set; }
}

public class AzureService
{
    public string? Source { get; set; }

    public string? Target { get; set; }

    public bool Fit { get; set; }
}
    




