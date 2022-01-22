# Generating the Azure-PlantUML distro

If you want to have customized builds and/or experiment with Azure-PlantUML, you can generate the Azure-PlantUML distro yourself.

## Prerequisites

The build applicaiton is supported on Windows, MacOS, and Linux (Ubuntu 20.04) with the following software pre-requisites installed:

* [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download)
* [PlantUML](https://plantuml.com/download)`

### Playwright CLI

Download and install the Playwright CLI by running the following command:

```bash
dotnet tool install -g Microsoft.Playwright.CLI
```

### Additional Setup for Linux (Ubuntu 20.04)

If your are building from Linux, install the following additional prerequisites:

```bash
# gdi+ libraries - required for the svg-net library
# see: http://svg-net.github.io/SVG/doc/Q&A.html#using-libgdiplus-on-ubuntu-linux
sudo add-apt-repository ppa:quamotion/ppa
sudo apt-get update
sudo apt-get install -y libgdiplus
```

For Linux systems, Playwright requires the installation of additinal dependencies. Navigate to the `scripts` folder and run:

```bash
playwright install-deps
```

## Configure

### Icon files

Download the [Microsoft Azure architecture icons](https://docs.microsoft.com/en-us/azure/architecture/icons/) and copy all folders from `Azure_Public_Service_Icons_V4\Azure_Public_Service_Icons\Icons` to [source/official](../source/official)

Place any icons that are not part of the Microsoft Azure Architecture bundle into [source/manual](../source/manual). For additional icons not in the official set, we suggest using the [Amazing Icon Downloader](https://chrome.google.com/webstore/detail/amazing-icon-downloader/kllljifcjfleikiipbkdcgllbllahaob/)

### Application Settings

Create a new file named `appsettings.json` within the `scripts` directory and save the following contents:

```json
{
    "sourceFolderPath": "..\\source",
    "targetFolderPath": "..\\dist",
    "monochromeColorHex": "#0072C6",
    "plantUmlPath": "C:\\ProgramData\\chocolatey\\lib\\plantuml\\tools\\plantuml.jar"
}
```

For convenience, sample settings are provided in [appsettings.example](appsettings.example). Ensure the values match your OS type and any file path difference.

### Configuration File: Config.yaml

This configuration file is used to map specific SVG file names to Azure services.
On top each Azure service is mapped to his primary category.

## Run

Use `dotnet run` from the `scripts` folder to execute the application.

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
