# Azure-PlantUML

![Basic usage - Stream processing with Azure Stream Analytics](http://www.plantuml.com/plantuml/png/fLBHJW8n47o_vFvXvGaJu30cFc1YGD5W12K8lfVU0eszzjBTIlJhxN4UoaLDZ8yxdPcTJjid5evzfk5Ia9BWIQmHsl383aK6kRCIYPHPmrRn1WPltc5rE312lxAI54TnT9JYIXai6TF2SCESKoz9dDXsra7ibvxGIMiO3NUapPAPaiAbzbvXZZhPATjJBGml9kCa4yJeabGH1tTbfFOfUgS_DvIAAvjbrdJo0Fp8guAMYkkN463abt_Hb5VUWxzAXuB5KX1I5P0oIyDgNHnfN36m1QVM6uPKEPIih2cE3l8rLy9XOLO1BMZS10WAh166wLqF9fWAhOCSHa8-ZvL4GmXFQ5BhRjDpE8NvWXh7TMjsAgzPpmFq3-jQGqqwd8FdnxUPxlzKiUWC7h-OL5qrhUVSSpK6tiCEWAhRGdhIwdQxC2lbSLHl2zhj2wYbYAFwnRzdrO0TwJ1IiR4VgWXl-Wu0 "Basic usage - Stream processing with Azure Stream Analytics")

[PlantUML](http://en.plantuml.com/) sprites, macros and stereotypes for creating PlantUML diagrams with [Azure](https://azure.microsoft.com/en-us/) components.

Azure-PlantUML includes symbols and useful macros for all Azure services.  
The official [Microsoft Azure, Cloud and Enterprise Symbol / Icon Set](http://aka.ms/CnESymbols) is used as the primary source. On top the missing Azure services symbols have been extracted [manually](source/manual/) from [Azure.com](https://azure.com/).

With Azure-PlantUML it is feasible to create visually appealing and memorable PlantUML diagrams for your Azure systems.

It is also possible to combine Azure-PlantUML with [C4-PlantUML](https://github.com/RicardoNiepel/C4-PlantUML) to create [C4 models](https://c4model.com/) for Azure architectures.

* [Getting Started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [List of all supported Azure Symbols](#list-of-all-supported-azure-symbols)
  * [Hello World](#hello-world)
* [Usages](#usages)
  * [Basic usage](#basic-usage)
  * [Raw sprite usage](#raw-sprite-usage)
  * [In combination with C4-PlantUML](#in-combination-with-c4-plantuml)
* [Advanced Samples](#advanced-samples)
* [Snippets for Visual Studio Code](#snippets-for-visual-studio-code)
* [Customized Builds](#customized-builds)
* [Built With](#built-with)
* [Contributing](#contributing)
* [License](#license)
* [Acknowledgments](#acknowledgments)

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
!includeurl https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist/AzureCommon.puml
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
!define AzurePuml https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Databases/all.puml
!includeurl AzurePuml/Compute/AzureFunction.puml
```

### List of all supported Azure Symbols

All Azure services names, categories, colored and monochrom symbols, and their `.puml` files can be found in the [Azure-PlantUML Azure Symbols Documentation](AzureSymbols.md#azure-symbols).

### Hello World

```csharp
@startuml Hello World
!define AzurePuml https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist
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

![Hello World](http://www.plantuml.com/plantuml/png/dP11JyCm38NlbVeVGaz3eisT0zgq2N4OK24GPpVrjaXkNCME4Fnwsgu2Ue03j_VYzpvRMOj2rDIHkKCYDgySgSspYnOFFg3PVAI8zJW-gVPoRMs4j-hezPIQ91WT1yMWbUFEFda7iUK7ZpsIdPfbJH3qvNvlEn35Q5ilEj1zS9HQJ96-DvmRM-uw3bK_FFoMsu520u9YWUZFv03ha-APV9k2K__5pZDmoj4KDKRXK1WGqqzoGnuapnQbrM4Mq_3A57jea8f1FACx1IO-le_kiHz3G6Q7ugAVN74vmxOi7SkkMRbP_1EMPpyEDMLvOnRNvTd2Je75nf6mnj0E__09 "Hello World")

## Usages

It is up to you how you want to use Azure-PlantUML.

It is possible to build very simple diagrams with it and leverage the Azure-PlantUML macros.  
You can also decide that you just want to use the Azure-PlantUML sprites.  
In addition it is also possible to use Azure-PlantUML in combination with [C4-PlantUML](https://github.com/RicardoNiepel/C4-PlantUML) for using the [C4 model](https://c4model.com/) and creating diagrams for large systems.

### Basic usage

Just import the necessary `.puml` files and you can use the macros in all your PlantUML diagrams.

```csharp
@startuml Basic usage - Stream processing with Azure Stream Analytics

!define AzurePuml https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Analytics/AzureEventHub.puml
!includeurl AzurePuml/Analytics/AzureStreamAnalytics.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml

left to right direction

agent devices

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

![Basic usage - Stream processing with Azure Stream Analytics](http://www.plantuml.com/plantuml/png/fLBHJW8n47o_vFvXvGaJu30cFc1YGD5W12K8lfVU0eszzjBTIlJhxN4UoaLDZ8yxdPcTJjid5evzfk5Ia9BWIQmHsl383aK6kRCIYPHPmrRn1WPltc5rE312lxAI54TnT9JYIXai6TF2SCESKoz9dDXsra7ibvxGIMiO3NUapPAPaiAbzbvXZZhPATjJBGml9kCa4yJeabGH1tTbfFOfUgS_DvIAAvjbrdJo0Fp8guAMYkkN463abt_Hb5VUWxzAXuB5KX1I5P0oIyDgNHnfN36m1QVM6uPKEPIih2cE3l8rLy9XOLO1BMZS10WAh166wLqF9fWAhOCSHa8-ZvL4GmXFQ5BhRjDpE8NvWXh7TMjsAgzPpmFq3-jQGqqwd8FdnxUPxlzKiUWC7h-OL5qrhUVSSpK6tiCEWAhRGdhIwdQxC2lbSLHl2zhj2wYbYAFwnRzdrO0TwJ1IiR4VgWXl-Wu0 "Basic usage - Stream processing with Azure Stream Analytics")

### Raw sprite usage

If you just want to use the PlantUML sprites inside your existing diagrams, this is also possible.

```csharp
@startuml Raw usage - Sprites

!define AzurePuml https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml
!includeurl AzurePuml/Compute/AzureFunction.puml

component "<color:red><$AzureFunction></color>" as myFunction

database "<color:#0072C6><$AzureCosmosDb></color>" as myCosmosDb

myFunction --> myCosmosDb

@enduml
```

![Raw usage - Sprites](http://www.plantuml.com/plantuml/png/VO_TIWCn44Rl5_OTOlLsDyKBXR8ibHQlL4h51vWcqpQGFp8JYZwzsTPjiQ2ttymvyysS619dPyKQ3Y8Jx50quZqcmqXrLLSt6hV6etZwoWbNm-AUET9CoWI7TcTudpUPCAdW6JstAZYvDWgI3cy68rfv99kL1SyVho_I0J4cgGrniHklRDQOaxrSE16Bu5pmRIp1NtjBODW08Oq4kK3BpRzCqSRCU2AUirTipbVgghIFmPS_nAHJmOOqIwZxxkxNTj_9uwoV223XFiTy4EYVIcV-TZfzl5yyZ8wnuxLZp0V7nIYQfhyQpj7hKlOR "Raw usage - Sprites")

### In combination with C4-PlantUML

Our recommmendation is to use Azure-PlantUML in combination with [C4-PlantUML](https://github.com/RicardoNiepel/C4-PlantUML).

Take a look into the [Advanced Samples](#advanced-samples) section to see the full power of Azure-PlantUML.

## Advanced Samples

The following advanced samples are reproductions from the official [Azure documentation](https://docs.microsoft.com/en-us/azure/) and [Azure architecture center](https://docs.microsoft.com/en-us/azure/architecture/).

All of them are created in combination with [C4-PlantUML](https://github.com/RicardoNiepel/C4-PlantUML) for using the [C4 model](https://c4model.com/):
> "a way for software development teams to efficiently and effectively communicate their software architecture, at different levels of detail, telling different stories to different types of audience, when doing up front design or retrospectively documenting an existing codebase"

### Azure IoT Reference Architecture - Stateful stream processing

Original: [Azure IoT Reference Architecture Guide](https://aka.ms/iotrefarchitecture)

Source: [C4 usage - IoT Reference Architecture - Stateful stream processing](samples/C4%20usage%20-%20IoT%20Reference%20Architecture%20-%20Stateful%20stream%20processing.puml)

![C4 usage - IoT Reference Architecture - Stateful stream processing](http://www.plantuml.com/plantuml/png/bLLDRziu4Bq7o7-OPWzr0DbEikqUUghhf8cBj3Ki1K5F0IsDRRQKg2Kf9-wltqTAbfMrDuiS4f2Z-NZpvWtzqJfXN9MhuwFZepUoI5MLSMKKhPmhxOVXq8Z7mLAwLJMlB9jK5uuBDqXrFfp9L9XCVvDSiXgEt_MdIXJkxkljC1VMiO7fOGn_8GisWt9R8-C533JwNHcUmlZgIY5ohrWchNlUGLkXZXZhFDT5W-Em7rpAod7j_xagKYV_9s9K2BLnChLrJEmCYxmrlYRqQerkRwhvYp6Nmecvi6otBTbSsykNOsAdZLWsJNrIUjuONWoQW7nJiFk-I5QoM3Rr9ZhP8Jm-kXtz-7wNFDnUVKuUPfFhcwGrtK-I6zYlhxvTUiTuWn7blOpNCcKRqSbbVJevzLyxgNjIUmxWWJlXT89Zx4IHOVuKd_lhf5WQjfPABGj72sr8A4ME5UVip2QYoW8npIkfN5yMr6IYcSPeRGWmECAdKtxBVe-RGxBBrw2brjG55T5xIkxGqpkQOf6aLuojOPB8id0dfycjU0VfxOnJbckccf6-q_rKwIgZ78rXDFOjBOpEjz0zpcUIxBNTRYqesCAZgT4WrxFWWQpF8ngL9SLiGXib5eUcVwul8Xh_uHsS4OK5BJb9H-1Kaw1CBXPik2Ou5ouY7Ym7z1ESUtBvoSj3ABZNtm6XHRbLPUzHcBmHMsr3iNlOg37wuXVHsTaPpUu2Ptuss7jRvMNTL2YApGRc3O5801DsDTSWlZEyVsrTppw_Xsbv2xKcN-mlYkkgeYpL1dNpKl4J_LsnaT29NhCX9tCG1eFEaCPfnKPu4IdyfrvL-ibUd0FKnfuI144GLbyl-jfaY6uSD5wxbQxCAGLnsVrn2kJrWknCSzIOmWJzg7QqX0b6z2NkKD3PwrwgLTQv8y4ObYxfrumN6TERLgRKbgake5L4ir7_UXO7LfIVBbZnyyxmxf3X57rP4eRnbwuWu0mkuJsNKEI6X2Eb7q6bXM0y7TwLXPN-MneqELSSLd0kNBgYSgVKqCkCrUwLQD-2c96gbJmetJblVGwjRRFmprw79XYX3vz4-gkDtvTCEFz6F0i03Vd3KZRXRUG-so_KxLG-WFZGLZUx_Dyuzf0V45DYv7B9fdxyW0u_SlK0aBRBvW401tVOoBR1xTi1ATJgRXD-nDJmG_4F "C4 usage - IoT Reference Architecture - Stateful stream processing")

### Azure Reference Architecture - Highly scalable web application

Original: [Azure Reference Architecture](https://docs.microsoft.com/en-us/azure/architecture/reference-architectures/app-service-web-app/scalable-web-app)

Source: [C4 usage - Highly scalable web application.puml](samples/C4%20usage%20-%20Highly%20scalable%20web%20application.puml)

![C4 usage - Highly scalable web application](http://www.plantuml.com/plantuml/png/dLLDRziu4Bq7o7-OzG8BBU3ORThJdkh8GJusTLpBtc9F1YNHCZSKgVB3hlFhEwH4MKwS8DcBDHhDcna-lXb_qOOeOqj-Ud9wyXiJ6RSvjOh3sfXA_pKSAh8T5CoiRMerLPaKXWepo6GvdB6Cg5nE6Aqe7yQVpwQS2BFuUZSiYJPKeMiPOpnXWgf15MhaT8KE63rQHQVeV7SbZtnMB6VQ79wWhL0ZObcMKZHz78yR4qDIegck4JEAIMEIhUbxKB7KfTJZz3sOv0SFdbTXtsbQ0z0OLTNROkFnv6s12IKgMxyQcnYfI47h-0ikqyRnTj0tIoqzH9sUt8t-lL_CbzzlvjVBkykhoydOUQTKQIaYfuO-z1RuwFtf_Ase8bqH_98mKN1K16O4PAM7hf4eB2S8kqqSfiDPjFKFZC5t-0D6qviMb4m7ayivn59H-FfF3EU3Zts8VtVVnfIp3VO3PaqXcOw0Y1mg9JSifnfcbybyPJbWRfTGmtRDiZKGp8CwrhQaEQoKB0EwxGdl9ifoWGNGyZqya98mICxTI-Qqfw6oAMSPtm7P4CP9odrMDnsmaWgut5By9UZ8ThwlbHy9oEkUTo1N0L0J-FJs8bm2XcKTZsFL-o1kWEYveiWvuJ2rgfAQmkQy3zVIzE5kbdGITsyu-k5U4EkTBoLfy1qHHje_N1lUW1kHqv-zkYsHupQ0vADxRQ-W1e2sHK6rOSXw7rRsyN478PLZ1fT6OR6lCrtHZAtmVE6hkoV9MOuqQY2WlQP2LkQgrpTOMcvOXPym4iFmc5ItpUytHgHQqrskUnvq7vzt60ih8yeQmmbEPYWRlEBnXGT-ktFAwiFd3nzWjjYNxAoEIAETDi4Dh6NhKwVUVX9kPTfC0bgzsaHBGpB-6pQVWlgL5Hfr9eepj0HI0uRe1usyU4Md97iebBGYhulCA6-cjXvG75tjqbpFvrD_q_k0TXfVYQa7v5c03rbEA2lMgMnsmRDqjydz15NQIRgym6vVWeKnFvBVTt5iK6QKvDgBOt_wa338VsgDxj04_pcEcJ0od3G0mXNkathTRXLpkiHujoF6zsvFFOLsnFCQkb7IK_I1Nb_4F-qsJEaH3fQBLc21XIkAB0ZwqxZpjz67iUyde0F-NnHqy6ycuPYIziLRMYuiuxdkh8zGSY-_9ZGS-oW2_o39RZbkW-js_OSlLEJu9_eB "C4 usage - Highly scalable web application")

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

If you have any ideas, just [open an issue][issues] and tell me what you think.

If you'd like to contribute, please fork the repository and use a feature branch.  
Pull requests are warmly welcome.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [AWS-PlantUML](https://github.com/milo-minderbinder/AWS-PlantUML) - for the base structure
* [plantuml-office](https://github.com/Roemer/plantuml-office) - for the scripts idea
* [C4 Model](https://c4model.com/) - for the hope that it's possible improve architecture documentations