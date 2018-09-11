public static void GenerateMarkdownTable(string distFolder)
{
    Console.WriteLine("Generating Markdown table...");

    var sbTable = new StringBuilder();
    sbTable.AppendLine("Category | Macro | Color | Mono  | Url");
    sbTable.AppendLine("  ---    |  ---  | :---:  | :---: | ---");

    var currentCategory = "";

    foreach (var filePath in Directory.GetFiles(distFolder, "*.puml", SearchOption.AllDirectories))
    {
        var entityName = Path.GetFileNameWithoutExtension(filePath);
        var category = Directory.GetParent(filePath).Name;

        if(currentCategory != category && "dist" != category)
        {
            sbTable.AppendLine($"**{category}** | | | | **{category}/all.puml**");
            currentCategory = category;
            continue;
        }

        sbTable.Append($"{category} |");
        sbTable.Append($"{entityName} |");

        if(File.Exists(Path.Combine(distFolder, $"{category}/{entityName}.png")))
        {
            sbTable.Append($"![{entityName}]({category}/{entityName}.png?raw=true) |");
        }  
        else 
        {
            sbTable.Append($" |");
        }
        
        sbTable.Append($"![{entityName}]({category}/{entityName}(m).png?raw=true) |");
        sbTable.AppendLine($"{category}/{entityName}.puml");
    }
    File.WriteAllText(Path.Combine(distFolder, "table.md"), sbTable.ToString());
}