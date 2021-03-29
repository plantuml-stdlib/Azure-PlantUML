# Azure-PlantUML

![Basic usage - Stream processing with Azure Stream Analytics](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FBasic%2520usage%2520-%2520Stream%2520processing%2520with%2520Azure%2520Stream%2520Analytics.puml)

[PlantUML](http://en.plantuml.com/) sprites, macros and stereotypes for creating PlantUML diagrams with [Azure](https://azure.microsoft.com/en-us/) components.

Azure-PlantUML includes symbols and useful macros for all Azure services.  
The official [Microsoft Azure, Cloud and Enterprise Symbol / Icon Set](http://aka.ms/CnESymbols) is used as the primary source. On top the missing Azure services symbols have been extracted [manually](source/manual/) from [Azure.com](https://azure.com/).

With Azure-PlantUML it is feasible to create visually appealing and memorable PlantUML diagrams for your Azure systems.

It is also possible to combine Azure-PlantUML with [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) to create [C4 models](https://c4model.com/) for Azure architectures.  

> See also [Save the world from Powerpoint Cloud Solution Architects](https://azure-development.com/2018/09/11/save-the-world-from-powerpoint-cloud-solution-architects/)

## Content <!-- {ignore=true} -->

<!-- @import "[TOC]" {cmd="toc" depthFrom=2 depthTo=6 orderedList=false} -->

<!-- code_chunk_output -->

* [Content <!-- {ignore=true} -->](#content-ignoretrue-)
* [Getting Started](#getting-started)
	* [Prerequisites](#prerequisites)
	* [List of all supported Azure Symbols](#list-of-all-supported-azure-symbols)
	* [Hello World](#hello-world)
* [Usages](#usages)
	* [Basic usage](#basic-usage)
	* [Raw sprite usage](#raw-sprite-usage)
	* [Simplified mode](#simplified-mode)
	* [In combination with C4-PlantUML](#in-combination-with-c4-plantuml)
* [Advanced Samples](#advanced-samples)
	* [C4 Integration](#c4-integration)
	* [Azure IoT Reference Architecture: Stateful stream processing](#azure-iot-reference-architecture-stateful-stream-processing)
	* [Azure Reference Architecture: Highly scalable web application](#azure-reference-architecture-highly-scalable-web-application)
* [Snippets for Visual Studio Code](#snippets-for-visual-studio-code)
* [Customized Builds](#customized-builds)
* [Built With](#built-with)
* [Contributing](#contributing)
* [License](#license)
* [Acknowledgments](#acknowledgments)

<!-- /code_chunk_output -->

## Getting Started

To be able to use Azure-PlantUML it is necessary to use specific `!includes`.  
After that the Azure service macros are available and can be used.  
A list of all supported Azure services can be found in the [Azure-PlantUML Azure Symbols Documentation](AzureSymbols.md).

### Prerequisites

At the top of your Azure-PlantUML `.puml` file, you need to include the `AzureCommon.puml` file found in the `dist` folder of this repo.

To be independent of any internet connectivity, you can also download `AzureCommon.puml` and reference it locally with

```c#
!include path/to/AzureCommon.puml
```

If you want to use the always up-to-date version in this repo, use the following:

```c#
!includeurl https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist/AzureCommon.puml
```

The next step is to include specific `.puml` files from Azure-PlantUML.  
For each Azure service a specific `.puml` file exists, which contains sprite and macros definitions.  
It is also possible to include Azure services category `.puml` files, which contain all Azure services from this category.

```c#
!define AzurePuml path/to
!include AzurePuml/AzureCommon.puml
!include AzurePuml/Databases/all.puml
!include AzurePuml/Compute/AzureFunction.puml
```

Or the always up-to-date version in this repo:

```c#
!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Databases/all.puml
!includeurl AzurePuml/Compute/AzureFunction.puml
```

### List of all supported Azure Symbols

All Azure services names, categories, colored and monochrom symbols, and their `.puml` files can be found in the [Azure-PlantUML Azure Symbols Documentation](AzureSymbols.md#azure-symbols).

### Hello World

```csharp
@startuml Hello World
!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Databases/all.puml
!includeurl AzurePuml/Compute/AzureFunction.puml

actor "Person" as personAlias
AzureFunction(functionAlias, "Label", "Technology", "Optional Description")
AzureCosmosDb(cosmosDbAlias, "Label", "Technology", "Optional Description")

personAlias --> functionAlias
functionAlias --> cosmosDbAlias

@enduml
```

![Hello World](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FHello%2520World.puml)

## Usages

It is up to you how you want to use Azure-PlantUML.

It is possible to build very simple diagrams with it and leverage the Azure-PlantUML macros.  
You can also decide that you just want to use the Azure-PlantUML sprites.  
In addition it is also possible to use Azure-PlantUML in combination with [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) for using the [C4 model](https://c4model.com/) and creating diagrams for large systems.

### Basic usage

Just import the necessary `.puml` files and you can use the macros in all your PlantUML diagrams.

```csharp
@startuml Basic usage - Stream processing with Azure Stream Analytics

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Analytics/AzureEventHub.puml
!includeurl AzurePuml/Analytics/AzureStreamAnalytics.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml

left to right direction

agent "Device Simulator" as devices #fff

AzureEventHub(fareDataEventHub, "Fare Data", "PK: Medallion HackLicense VendorId; 3 TUs")
AzureEventHub(tripDataEventHub, "Trip Data", "PK: Medallion HackLicense VendorId; 3 TUs")
AzureStreamAnalytics(streamAnalytics, "Stream Processing", "6 SUs")
AzureCosmosDb(outputCosmosDb, "Output Database", "1,000 RUs")

devices --> fareDataEventHub
devices --> tripDataEventHub
fareDataEventHub --> streamAnalytics
tripDataEventHub --> streamAnalytics
streamAnalytics --> outputCosmosDb

@enduml
```

![Basic usage - Stream processing with Azure Stream Analytics](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FBasic%2520usage%2520-%2520Stream%2520processing%2520with%2520Azure%2520Stream%2520Analytics.puml)

### Raw sprite usage

If you just want to use the PlantUML sprites inside your existing diagrams, this is also possible.

```csharp
@startuml Raw usage - Sprites
!pragma revision 1

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureRaw.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml
!includeurl AzurePuml/Compute/AzureFunction.puml


component "<color:red><$AzureFunction></color>" as myFunction

database "<color:#0072C6><$AzureCosmosDb></color>" as myCosmosDb

AzureFunction(mySecondFunction, "Stream Processing", "Consumption")

rectangle "<color:AZURE_SYMBOL_COLOR><$AzureCosmosDb></color>" as mySecondCosmosDb

myFunction --> myCosmosDb

mySecondFunction --> mySecondCosmosDb

@enduml
```

![Raw usage - Sprites](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FRaw%2520usage%2520-%2520Sprites.puml)

### Simplified mode

Sometimes your architecture diagram includes to many information for your target audience. We need a management version if - something which can be used inside a presentation in front of the management round.

> Please always include the technical one in the appendix and ensure that everybody knows, that this is only a simplified version.

To enable the simplified mode `AzureSimplified.puml` needs to be included or can just be commented in/out.

```csharp
@startuml Two Mode Sample
!pragma revision 1

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureCommon.puml

' !includeurl AzurePuml/AzureSimplified.puml

!includeurl AzurePuml/Analytics/AzureEventHub.puml
!includeurl AzurePuml/Compute/AzureFunction.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml
!includeurl AzurePuml/Storage/AzureDataLakeStorage.puml
!includeurl AzurePuml/Analytics/AzureStreamAnalytics.puml
!includeurl AzurePuml/InternetOfThings/AzureTimeSeriesInsights.puml
!includeurl AzurePuml/Identity/AzureActiveDirectoryB2C.puml
!includeurl AzurePuml/DevOps/AzureApplicationInsights.puml


LAYOUT_LEFT_RIGHT

AzureEventHub(rawEventsHubAlias, "Raw Event Hub", "PK: Medallion HackLicense VendorId; 3 TUs")
AzureDataLakeStorage(datalakeAlias, "Data Lake", "GRS")
AzureStreamAnalytics(streamAnalyticsAlias, "Aggregate Events", "6 SUs")
AzureFunction(stateFunctionAlias, "State Processor", "C#, Consumption Plan")
AzureEventHub(aggregatedEventsHubAlias, "Aggregated Hub", "6 TUs")
AzureCosmosDb(stateDBAlias, "State Database", "SQL API, 1000 RUs")
AzureTimeSeriesInsights(timeSeriesAlias, "Time Series", "2 Data Processing Units")

rawEventsHubAlias ----> datalakeAlias
rawEventsHubAlias --> streamAnalyticsAlias
rawEventsHubAlias ---> stateFunctionAlias
streamAnalyticsAlias --> aggregatedEventsHubAlias
aggregatedEventsHubAlias --> timeSeriesAlias
stateFunctionAlias --> stateDBAlias

@enduml
```

![Two Mode Sample - Simplified](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FTwo%2520Mode%2520Sample%2520-%2520Simplified.puml)

![Two Mode Sample - Normal](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FTwo%2520Mode%2520Sample%2520-%2520Normal.puml)

### In combination with C4-PlantUML

Our recommmendation is to use Azure-PlantUML in combination with [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML).

Take a look into the [Advanced Samples](#advanced-samples) section to see the full power of Azure-PlantUML.

## Advanced Samples

The following advanced samples are reproductions from the official [Azure documentation](https://docs.microsoft.com/en-us/azure/) and [Azure architecture center](https://docs.microsoft.com/en-us/azure/architecture/).

All of them are created in combination with [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) for using the [C4 model](https://c4model.com/):
> "a way for software development teams to efficiently and effectively communicate their software architecture, at different levels of detail, telling different stories to different types of audience, when doing up front design or retrospectively documenting an existing codebase"

### C4 Integration

For using **Azure-PlantUML** and [C4-PlantUML](https://github.com/plantuml-stdlib/C4-PlantUML) together, you need to include `AzureC4Integration.puml`.
```csharp
@startuml

!includeurl https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-1/dist
!includeurl AzurePuml/AzureCommon.puml

!includeurl AzurePuml/AzureC4Integration.puml

!includeurl AzurePuml/Databases/AzureRedisCache.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml

....

@enduml
```

### Azure IoT Reference Architecture: Stateful stream processing

Original: [Azure IoT Reference Architecture Guide](https://aka.ms/iotrefarchitecture)

Source: [C4 usage - IoT Reference Architecture - Stateful stream processing](samples/C4%20usage%20-%20IoT%20Reference%20Architecture%20-%20Stateful%20stream%20processing.puml)

![C4 usage - IoT Reference Architecture - Stateful stream processing](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FC4%2520usage%2520-%2520IoT%2520Reference%2520Architecture%2520-%2520Stateful%2520stream%2520processing.puml)

### Azure Reference Architecture: Highly scalable web application

Original: [Azure Reference Architecture](https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/app-service-web-app/scalable-web-app)

Source: [C4 usage - Highly scalable web application.puml](samples/C4%20usage%20-%20Highly%20scalable%20web%20application.puml)

![C4 usage - Highly scalable web application](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Fplantuml-stdlib%2FAzure-PlantUML%2Fmaster%2Fsamples%2FC4%2520usage%2520-%2520Highly%2520scalable%2520web%2520application.puml)

## Snippets for Visual Studio Code

Because the PlantUML support inside of Visual Studio Code is excellend with the [PlantUML extension](https://marketplace.visualstudio.com/items?itemName=jebbs.plantuml), you can also find VS Code snippets for Azure-PlantUML at [dist/.vscode/snippets/diagram.json](dist/.vscode/snippets/diagram.json).

It is possible to save them directly inside VS Code: [Creating your own snippets](https://code.visualstudio.com/docs/editor/userdefinedsnippets#_creating-your-own-snippets).

Or you can use the [Project Snippets extension](https://marketplace.visualstudio.com/items?itemName=rebornix.project-snippets).  
Now it is possible to have workspace/project level code snippets.

## Customized Builds

It is also possible to customize the Azure-PlantUML distro generation.  
All details can be found in the [Generating the Azure-PlantUML distro documentation](scripts/README.md).

## Built With

* [dotnet script](https://github.com/filipw/dotnet-script) - C# script runtime
* [YamlDotNet](https://github.com/aaubry/YamlDotNet) - .NET library for YAML config parsing
* [Json.NET](https://github.com/JamesNK/Newtonsoft.Json) - .NET library for JSON

## Contributing

If you have any ideas, just [open an issue](https://github.com/plantuml-stdlib/Azure-PlantUML/issues/new) and tell me what you think.

If you'd like to contribute, please fork the repository and use a feature branch.  
Pull requests are warmly welcome.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [AWS-PlantUML](https://github.com/milo-minderbinder/AWS-PlantUML) - for the base structure
* [plantuml-office](https://github.com/Roemer/plantuml-office) - for the scripts idea
* [C4 Model](https://c4model.com/) - for the hope that it's possible to improve architecture documentations

