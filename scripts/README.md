# Generating the Azure-PlantUML distro

If you want to have customized builds and/or experiment with Azure-PlantUML, you can generate the Azure-PlantUML distro yourself.

## Prerequisites

* You need [dotnet script](https://github.com/filipw/dotnet-script) installed for executing the scripts
* You also need to download the [Microsoft Azure, Cloud and Enterprise Symbol / Icon Set](http://aka.ms/CnESymbols) and copy all files from `Symbols\CnE_Cloud\SVG` to [source/official](../source/official)

## Configure

It is required to have [PlantUML](http://plantuml.com/) and [Inkscape](https://inkscape.org/) installed.
Please make sure, that the following variables at the beginning of `main.csx` are correct configured for your system:

```csharp
var plantUmlPath = @"C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar";
var inkScapePath = @"C:\Program Files\Inkscape\inkscape.exe";
```

### Configuration File: Config.yaml

This configuration file is used to map specific SVG file names to Azure services.
On top each Azure service is mapped to his primary category.

## Run

You can just execute

```text
dotnet script main.csx
```

### What happens

From a logical point of view, the following happens:

1. The `Config.yaml` is loaded
2. Cleanup: all files and directories from `dist` folder are deleted
3. AzureCommon.puml is copied to `dist`
4. For all configured services in `Config.yaml` a monochom and a colored SVGs is searched
    * The `source/manual` folder is used to supplement the original SVGs from Microsoft
    * If no colored SVG is found, this will be ignored
    * If no monochrom SVG is found, this will be generated form the colored one
    * If both is not found, an error is shown
5. From colored SVG a PNG is generated
6. From the monochrom SVG a PNG with white background is generated
    * This is needed for PlantUML sprite generation
7. A PlantUML sprite is generated
8. In a second round a PNG without background is generated from the monochrom SVG

        During all generations, it is ensured that the max size of length and width are not exeeding `targetMaxSize`.

9. In addition to single Azure services PUML files, also a combined PUML file per category is generated
10. A markdown table with all Azure services, their colored and monochrom symbols and the PUML files is generated
11. VSCode snippets for all Azure services for their PlantUML usage are generated