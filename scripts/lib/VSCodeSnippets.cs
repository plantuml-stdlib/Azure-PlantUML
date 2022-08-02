using System.Text.RegularExpressions;
using Newtonsoft.Json;

public static class VSCodeSnippets
{
    public static async Task<bool> GenerateSnippets(string distFolder)
    {
        Console.WriteLine("Generating VSCode Snippets...");

        var snippets = new Dictionary<string, Snippet>();

        foreach (var filePath in Directory.GetFiles(distFolder, "*.puml", SearchOption.AllDirectories))
        {
            var entityName = Path.GetFileNameWithoutExtension(filePath);
            if (entityName == "all")
            {
                continue;
            }

            snippets.Add($"{entityName}", new Snippet{
                prefix = $"{SplitCamelCase(entityName)}",
                description = $"Add {SplitCamelCase(entityName)} to diagram",
                body = new List<string>{
                    $"{entityName}(${{1:alias}}, \"${{2:label}}\", \"${{3:technology}}\")",
                    "$0"
                }
            });

            snippets.Add($"{entityName}_Descr", new Snippet{
                prefix = $"{SplitCamelCase(entityName)} with Description",
                description = $"Add {SplitCamelCase(entityName)} with Description to diagram",
                body = new List<string>{
                    $"{entityName}(${{1:alias}}, \"${{2:label}}\", \"${{3:technology}}\", \"${{4:description}}\")",
                    "$0"
                }
            });
        }

        var snippetsDirectory = Path.Combine(distFolder, ".vscode", "snippets");
        Directory.CreateDirectory(snippetsDirectory);

        using (StreamWriter file = File.CreateText(Path.Combine(snippetsDirectory, "diagram.json")))
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
            };
            serializer.Serialize(file, snippets);
        }

        return await Task.FromResult(true);
    }

    private static string SplitCamelCase(string camelCaseString) => Regex.Replace(camelCaseString, "(\\B[A-Z])", " $1");

    private class Snippet
    {
        public string? prefix { get; set; }

        public string? description { get; set; }

        public List<string>? body { get; set; }
    }

}



