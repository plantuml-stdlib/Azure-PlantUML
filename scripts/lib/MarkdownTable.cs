using System.Text;
using System.Text.RegularExpressions;


public static class MarkdownTable
{
    public static async Task GenerateTable(string distFolder)
    {
        Console.WriteLine("Generating Markdown table...");

        var sbTable = new StringBuilder();
        sbTable.AppendLine("Category | Macro (Name) | <pre>Color</pre> | <pre>Mono </pre> | Url");
        sbTable.AppendLine("  ---    |  ---  | :---:  | :---: | ---");

        var currentCategory = "";

        foreach (var filePath in Directory.GetFiles(distFolder, "*.puml", SearchOption.AllDirectories))
        {
            var entityName = Path.GetFileNameWithoutExtension(filePath);
            var category = Directory.GetParent(filePath)!.Name;

            if("dist" == category)
            {
                continue;
            }
            if(currentCategory != category)
            {
                sbTable.AppendLine($"**{category}** | | | | **{category}/all.puml**");
                currentCategory = category;
                continue;
            }

            sbTable.Append($"{category} | ");
            sbTable.Append($"{entityName} </br> ({Regex.Replace(entityName, "(?<!^)(([A-Z][a-z])|([A-Z][A-Z0-9][A-Z])|([A-Z][A-Z]?[a-z][A-Z]))", " $1")}) | ");

            if(File.Exists(Path.Combine(distFolder, $"{category}/{entityName}.png")))
            {
                sbTable.Append($"![{entityName}](dist/{category}/{entityName}.png?raw=true) | ");
            }  
            else 
            {
                sbTable.Append($" |");
            }
            
            sbTable.Append($"![{entityName}](dist/{category}/{entityName}(m).png?raw=true) | ");
            sbTable.AppendLine($"{category}/{entityName}.puml");
        }

        await File.WriteAllTextAsync(Path.Combine(distFolder, "table.md"), sbTable.ToString());
    }

}

