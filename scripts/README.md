# Generating the Azure-PlantUML distro

If you want to have customized builds and/or experiment with Azure-PlantUML, you can generate the Azure-PlantUML distro yourself.

## Prerequisites

The build application is supported on Windows, macOS, and Linux with the following software installed:

* [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download)
* Java 11 or later
* [PlantUML](https://plantuml.com/download) (`plantuml.jar`)

The same steps run in CI, see [.github/workflows/build.yml](../.github/workflows/build.yml).

### Playwright browser

PNGs are rendered with headless Chromium through [Playwright](https://playwright.dev/dotnet/). The generator installs Chromium on first run. On Linux, the browser also needs system libraries. Install them once from the `scripts` folder (requires [PowerShell](https://learn.microsoft.com/powershell/scripting/install/installing-powershell)):

```bash
dotnet build
pwsh bin/Debug/net10.0/playwright.ps1 install --with-deps chromium
```

## Configure

### Add Icon files

Download the [Microsoft Azure architecture icons](https://learn.microsoft.com/azure/architecture/icons/) and copy all folders from `Azure_Public_Service_Icons_V4\Azure_Public_Service_Icons\Icons` to [source/official](../source/official)

Place any icons that are not part of the Microsoft Azure Architecture bundle into [source/manual](../source/manual). For additional icons not in the official set, we suggest using the [Amazing Icon Downloader](https://chrome.google.com/webstore/detail/amazing-icon-downloader/kllljifcjfleikiipbkdcgllbllahaob/)

### Application Settings

The generator needs four settings. Pass them on the command line, or put them in an `appsettings.json` file in the `scripts` directory (it is ignored by git):

```json
{
    "sourceFolderPath": "../source",
    "targetFolderPath": "../dist",
    "monochromeColorHex": "#0072C6",
    "plantUmlPath": "/path/to/plantuml.jar"
}
```

> The path to PlantUML depends on how you installed it, for example `C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar` with Chocolatey or `/usr/share/plantuml/plantuml.jar` with apt. On Windows, use `..\source` and `..\dist` for the folder paths.

### Update Config.yaml

Update this file to add or remove icons in your distro. The `source` attribute represents the svg file name. Logic has been added to the PlantUML generation program to simplify the value used here. Dashes, spaces, and certain prefixes and suffixes are removed when searching for source files. For example, adding **App Service Plan Linux** will work for the following source files:

* <b>00046-icon-service-</b>App-Service-Plan-Linux.svg
* App-Service-Plan-Linux<b>_COLOR</b>.svg
* App-Service-Plan-Linux.svg


The `target` attribute will be used for the PlantUML element stereotype and all target file names. Keep in mind that services are grouped into categories as reflected in the YAML structure.

## Generate PlantUML Elements

Use `dotnet run` from the `scripts` folder to execute the application. To pass the settings on the command line instead of `appsettings.json`:

```bash
dotnet run -- --sourceFolderPath=../source --targetFolderPath=../dist --monochromeColorHex=#0072C6 --plantUmlPath=/path/to/plantuml.jar
```

The program exits with a non-zero code if a setting is missing, the browser can't start, or any service in `Config.yaml` has no source SVG or fails to generate. The log lists the affected services at the end.

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
1. In addition to single Azure services PUML files, also a combined PUML file per category is generated
1. A markdown table with all Azure services, their colored and monochrome symbols and the PUML files is generated
1. VSCode snippets for all Azure services for their PlantUML usage are generated

## Update the Azure Symbols page

When the program completes, replace the markdown table in [AzureSymbols.md](https://github.com/plantuml-stdlib/Azure-PlantUML/blob/release/2-2/AzureSymbols.md?plain=1#L34) with the contents from `table.md` located in your target folder.
