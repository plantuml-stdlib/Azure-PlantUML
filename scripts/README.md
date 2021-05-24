# Generating the Azure-PlantUML distro

If you want to have customized builds and/or experiment with Azure-PlantUML, you can generate the Azure-PlantUML distro yourself.

## Prerequisites

* You need [dotnet script](https://github.com/filipw/dotnet-script) installed for executing the scripts (`dotnet tool install -g dotnet-script --version 0.53`)
* You also need to download the [Microsoft Azure architecture icons](https://docs.microsoft.com/en-us/azure/architecture/icons/) and copy all folders from `Azure_Public_Service_Icons_V4\Azure_Public_Service_Icons\Icons` to [source/official](../source/official)
* For additional icons not in the official set, we suggest using the [Amazing Icon Downloader](https://chrome.google.com/webstore/detail/amazing-icon-downloader/kllljifcjfleikiipbkdcgllbllahaob/)

## Configure

It is required to have [PlantUML](https://https://plantuml.com/) (`choco install plantuml`) , [Inkscape](https://inkscape.org/) and `rsvg_convert` (`choco install rsvg-convert`)installed.

Please make sure, that the following variables at the beginning of `main.csx` are correct configured for your system, if you used chocolately for `PlantUml` and `rsvg_convert` and the inkscape installer, they should be correct:

```csharp
var plantUmlPath = @"C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar";
var inkScapePath = @"C:\Program Files\Inkscape\inkscape.exe";
static string rsvgConvertPath = @"C:\ProgramData\chocolatey\lib\rsvg-convert\tools\rsvg-convert.exe";
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

1. The `official` folder is processed recursively to rename all the SVGs found
1. The `Config.yaml` is loaded
1. Cleanup: all files and directories from `dist` folder are deleted
1. AzureCommon.puml is copied to `dist`
1. For all configured services in `Config.yaml` a monochrome and a colored SVGs is searched
    * The `source/manual` folder is used to supplement the original SVGs from Microsoft
    * If no colored SVG is found, this will be ignored
    * If no monochrome SVG is found, this will be generated form the colored one
    * If both is not found, an error is shown
1. From colored SVG a PNG is generated
1. From the monochrome SVG a PNG with white background is generated
    * This is needed for PlantUML sprite generation
1. A PlantUML sprite is generated
1. In a second round a PNG without background is generated from the monochrome SVG

        During all generations, it is ensured that the max size of length and width are not exeeding `targetMaxSize`.

1. In addition to single Azure services PUML files, also a combined PUML file per category is generated
1. A markdown table with all Azure services, their colored and monochrome symbols and the PUML files is generated
1. VSCode snippets for all Azure services for their PlantUML usage are generated