# Azure Symbols

The following table lists all Azure symbols which are part of Azure-PlantUML.
They are categorized using their primary category.

If you want to be independent of any internet connectifity, you can also download the whole [Azure-PlantUML dist](dist/) and reference it locally with

```c#
!define AzurePuml path/to
!include AzurePuml/AzureCommon.puml
!include AzurePuml/Databases/AzureCosmosDb.puml
!include AzurePuml/Compute/AzureFunction.puml
```

If you want to use the always up-to-date version in this repo, use the following:

```c#
!define AzurePuml https://raw.githubusercontent.com/RicardoNiepel/Azure-PlantUML/master/dist
!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml
!includeurl AzurePuml/Compute/AzureFunction.puml
```

## SVG and PNG images, PPTX file

You can also use the colored and monochrom SVG and PNG images outside of PlantUML, e.g. for documents or presentations.

There is also a PowerPoint file (PPTX) which includes alls Azure symbols as SVGs: [Azure_Symbols.pptx](dist/Azure_Symbols.pptx?raw=true).

> If the size of the generated PNG images is not enough for you, take a look at [how you can build Azure-PlantUML on your own](scripts/README.md).

## All generated Azure symbols (SVG and PNG) and PlantUML sprites

Category | Macro (Name) | <pre>Color</pre> | <pre>Mono </pre> | Url
  ---    |  ---  | :---:  | :---: | ---
**AIMachineLearning** | | | | **AIMachineLearning/all.puml**
AIMachineLearning | AzureBatchAI </br> (Azure BatchAI) | ![AzureBatchAI](dist/AIMachineLearning/AzureBatchAI.png?raw=true) | ![AzureBatchAI](dist/AIMachineLearning/AzureBatchAI(m).png?raw=true) | AIMachineLearning/AzureBatchAI.puml
AIMachineLearning | AzureBotService </br> (Azure Bot Service) | ![AzureBotService](dist/AIMachineLearning/AzureBotService.png?raw=true) | ![AzureBotService](dist/AIMachineLearning/AzureBotService(m).png?raw=true) | AIMachineLearning/AzureBotService.puml
AIMachineLearning | AzureCognitiveServices </br> (Azure Cognitive Services) | ![AzureCognitiveServices](dist/AIMachineLearning/AzureCognitiveServices.png?raw=true) | ![AzureCognitiveServices](dist/AIMachineLearning/AzureCognitiveServices(m).png?raw=true) | AIMachineLearning/AzureCognitiveServices.puml
AIMachineLearning | AzureMachineLearningService </br> (Azure Machine Learning Service) | ![AzureMachineLearningService](dist/AIMachineLearning/AzureMachineLearningService.png?raw=true) | ![AzureMachineLearningService](dist/AIMachineLearning/AzureMachineLearningService(m).png?raw=true) | AIMachineLearning/AzureMachineLearningService.puml
AIMachineLearning | AzureMachineLearningStudio </br> (Azure Machine Learning Studio) | ![AzureMachineLearningStudio](dist/AIMachineLearning/AzureMachineLearningStudio.png?raw=true) | ![AzureMachineLearningStudio](dist/AIMachineLearning/AzureMachineLearningStudio(m).png?raw=true) | AIMachineLearning/AzureMachineLearningStudio.puml
AIMachineLearning | MicrosoftGenomics </br> (Microsoft Genomics) | ![MicrosoftGenomics](dist/AIMachineLearning/MicrosoftGenomics.png?raw=true) | ![MicrosoftGenomics](dist/AIMachineLearning/MicrosoftGenomics(m).png?raw=true) | AIMachineLearning/MicrosoftGenomics.puml
**Analytics** | | | | **Analytics/all.puml**
Analytics | AzureAnalysisServices </br> (Azure Analysis Services) | ![AzureAnalysisServices](dist/Analytics/AzureAnalysisServices.png?raw=true) | ![AzureAnalysisServices](dist/Analytics/AzureAnalysisServices(m).png?raw=true) | Analytics/AzureAnalysisServices.puml
Analytics | AzureDatabricks </br> (Azure Databricks) | ![AzureDatabricks](dist/Analytics/AzureDatabricks.png?raw=true) | ![AzureDatabricks](dist/Analytics/AzureDatabricks(m).png?raw=true) | Analytics/AzureDatabricks.puml
Analytics | AzureDataCatalog </br> (Azure Data Catalog) | ![AzureDataCatalog](dist/Analytics/AzureDataCatalog.png?raw=true) | ![AzureDataCatalog](dist/Analytics/AzureDataCatalog(m).png?raw=true) | Analytics/AzureDataCatalog.puml
Analytics | AzureDataExplorer </br> (Azure Data Explorer) | ![AzureDataExplorer](dist/Analytics/AzureDataExplorer.png?raw=true) | ![AzureDataExplorer](dist/Analytics/AzureDataExplorer(m).png?raw=true) | Analytics/AzureDataExplorer.puml
Analytics | AzureDataLakeAnalytics </br> (Azure Data Lake Analytics) | ![AzureDataLakeAnalytics](dist/Analytics/AzureDataLakeAnalytics.png?raw=true) | ![AzureDataLakeAnalytics](dist/Analytics/AzureDataLakeAnalytics(m).png?raw=true) | Analytics/AzureDataLakeAnalytics.puml
Analytics | AzureEventHub </br> (Azure Event Hub) | ![AzureEventHub](dist/Analytics/AzureEventHub.png?raw=true) | ![AzureEventHub](dist/Analytics/AzureEventHub(m).png?raw=true) | Analytics/AzureEventHub.puml
Analytics | AzureHDInsight </br> (Azure HDInsight) | ![AzureHDInsight](dist/Analytics/AzureHDInsight.png?raw=true) | ![AzureHDInsight](dist/Analytics/AzureHDInsight(m).png?raw=true) | Analytics/AzureHDInsight.puml
Analytics | AzureStreamAnalytics </br> (Azure Stream Analytics) | ![AzureStreamAnalytics](dist/Analytics/AzureStreamAnalytics.png?raw=true) | ![AzureStreamAnalytics](dist/Analytics/AzureStreamAnalytics(m).png?raw=true) | Analytics/AzureStreamAnalytics.puml
**Compute** | | | | **Compute/all.puml**
Compute | AzureAppService </br> (Azure App Service) | ![AzureAppService](dist/Compute/AzureAppService.png?raw=true) | ![AzureAppService](dist/Compute/AzureAppService(m).png?raw=true) | Compute/AzureAppService.puml
Compute | AzureBatch </br> (Azure Batch) | ![AzureBatch](dist/Compute/AzureBatch.png?raw=true) | ![AzureBatch](dist/Compute/AzureBatch(m).png?raw=true) | Compute/AzureBatch.puml
Compute | AzureFunction </br> (Azure Function) | ![AzureFunction](dist/Compute/AzureFunction.png?raw=true) | ![AzureFunction](dist/Compute/AzureFunction(m).png?raw=true) | Compute/AzureFunction.puml
Compute | AzureServiceFabric </br> (Azure Service Fabric) | ![AzureServiceFabric](dist/Compute/AzureServiceFabric.png?raw=true) | ![AzureServiceFabric](dist/Compute/AzureServiceFabric(m).png?raw=true) | Compute/AzureServiceFabric.puml
Compute | AzureVirtualMachine </br> (Azure Virtual Machine) | ![AzureVirtualMachine](dist/Compute/AzureVirtualMachine.png?raw=true) | ![AzureVirtualMachine](dist/Compute/AzureVirtualMachine(m).png?raw=true) | Compute/AzureVirtualMachine.puml
Compute | AzureVirtualMachineScaleSet </br> (Azure Virtual Machine Scale Set) | ![AzureVirtualMachineScaleSet](dist/Compute/AzureVirtualMachineScaleSet.png?raw=true) | ![AzureVirtualMachineScaleSet](dist/Compute/AzureVirtualMachineScaleSet(m).png?raw=true) | Compute/AzureVirtualMachineScaleSet.puml
**Containers** | | | | **Containers/all.puml**
Containers | AzureContainerInstance </br> (Azure Container Instance) | ![AzureContainerInstance](dist/Containers/AzureContainerInstance.png?raw=true) | ![AzureContainerInstance](dist/Containers/AzureContainerInstance(m).png?raw=true) | Containers/AzureContainerInstance.puml
Containers | AzureContainerRegistry </br> (Azure Container Registry) | ![AzureContainerRegistry](dist/Containers/AzureContainerRegistry.png?raw=true) | ![AzureContainerRegistry](dist/Containers/AzureContainerRegistry(m).png?raw=true) | Containers/AzureContainerRegistry.puml
Containers | AzureKubernetesService </br> (Azure Kubernetes Service) | ![AzureKubernetesService](dist/Containers/AzureKubernetesService.png?raw=true) | ![AzureKubernetesService](dist/Containers/AzureKubernetesService(m).png?raw=true) | Containers/AzureKubernetesService.puml
Containers | AzureServiceFabricMesh </br> (Azure Service Fabric Mesh) | ![AzureServiceFabricMesh](dist/Containers/AzureServiceFabricMesh.png?raw=true) | ![AzureServiceFabricMesh](dist/Containers/AzureServiceFabricMesh(m).png?raw=true) | Containers/AzureServiceFabricMesh.puml
Containers | AzureWebAppForContainers </br> (Azure Web App For Containers) | ![AzureWebAppForContainers](dist/Containers/AzureWebAppForContainers.png?raw=true) | ![AzureWebAppForContainers](dist/Containers/AzureWebAppForContainers(m).png?raw=true) | Containers/AzureWebAppForContainers.puml
**Databases** | | | | **Databases/all.puml**
Databases | AzureCosmosDb </br> (Azure Cosmos Db) | ![AzureCosmosDb](dist/Databases/AzureCosmosDb.png?raw=true) | ![AzureCosmosDb](dist/Databases/AzureCosmosDb(m).png?raw=true) | Databases/AzureCosmosDb.puml
Databases | AzureDatabaseForMariaDB </br> (Azure Database For MariaDB) | ![AzureDatabaseForMariaDB](dist/Databases/AzureDatabaseForMariaDB.png?raw=true) | ![AzureDatabaseForMariaDB](dist/Databases/AzureDatabaseForMariaDB(m).png?raw=true) | Databases/AzureDatabaseForMariaDB.puml
Databases | AzureDatabaseForMySQL </br> (Azure Database For My SQL) | ![AzureDatabaseForMySQL](dist/Databases/AzureDatabaseForMySQL.png?raw=true) | ![AzureDatabaseForMySQL](dist/Databases/AzureDatabaseForMySQL(m).png?raw=true) | Databases/AzureDatabaseForMySQL.puml
Databases | AzureDatabaseForPostgreSQL </br> (Azure Database For Postgre SQL) | ![AzureDatabaseForPostgreSQL](dist/Databases/AzureDatabaseForPostgreSQL.png?raw=true) | ![AzureDatabaseForPostgreSQL](dist/Databases/AzureDatabaseForPostgreSQL(m).png?raw=true) | Databases/AzureDatabaseForPostgreSQL.puml
Databases | AzureDataFactory </br> (Azure Data Factory) | ![AzureDataFactory](dist/Databases/AzureDataFactory.png?raw=true) | ![AzureDataFactory](dist/Databases/AzureDataFactory(m).png?raw=true) | Databases/AzureDataFactory.puml
Databases | AzureRedisCache </br> (Azure Redis Cache) | ![AzureRedisCache](dist/Databases/AzureRedisCache.png?raw=true) | ![AzureRedisCache](dist/Databases/AzureRedisCache(m).png?raw=true) | Databases/AzureRedisCache.puml
Databases | AzureSqlDatabase </br> (Azure Sql Database) | ![AzureSqlDatabase](dist/Databases/AzureSqlDatabase.png?raw=true) | ![AzureSqlDatabase](dist/Databases/AzureSqlDatabase(m).png?raw=true) | Databases/AzureSqlDatabase.puml
Databases | AzureSqlDataWarehouse </br> (Azure Sql Data Warehouse) | ![AzureSqlDataWarehouse](dist/Databases/AzureSqlDataWarehouse.png?raw=true) | ![AzureSqlDataWarehouse](dist/Databases/AzureSqlDataWarehouse(m).png?raw=true) | Databases/AzureSqlDataWarehouse.puml
Databases | AzureSqlStretchDatabase </br> (Azure Sql Stretch Database) | ![AzureSqlStretchDatabase](dist/Databases/AzureSqlStretchDatabase.png?raw=true) | ![AzureSqlStretchDatabase](dist/Databases/AzureSqlStretchDatabase(m).png?raw=true) | Databases/AzureSqlStretchDatabase.puml
**DevOps** | | | | **DevOps/all.puml**
DevOps | AzureApplicationInsights </br> (Azure Application Insights) | ![AzureApplicationInsights](dist/DevOps/AzureApplicationInsights.png?raw=true) | ![AzureApplicationInsights](dist/DevOps/AzureApplicationInsights(m).png?raw=true) | DevOps/AzureApplicationInsights.puml
DevOps | AzureArtifacts </br> (Azure Artifacts) | ![AzureArtifacts](dist/DevOps/AzureArtifacts.png?raw=true) | ![AzureArtifacts](dist/DevOps/AzureArtifacts(m).png?raw=true) | DevOps/AzureArtifacts.puml
DevOps | AzureBoards </br> (Azure Boards) | ![AzureBoards](dist/DevOps/AzureBoards.png?raw=true) | ![AzureBoards](dist/DevOps/AzureBoards(m).png?raw=true) | DevOps/AzureBoards.puml
DevOps | AzureDevOps </br> (Azure Dev Ops) | ![AzureDevOps](dist/DevOps/AzureDevOps.png?raw=true) | ![AzureDevOps](dist/DevOps/AzureDevOps(m).png?raw=true) | DevOps/AzureDevOps.puml
DevOps | AzureDevTestLabs </br> (Azure Dev Test Labs) | ![AzureDevTestLabs](dist/DevOps/AzureDevTestLabs.png?raw=true) | ![AzureDevTestLabs](dist/DevOps/AzureDevTestLabs(m).png?raw=true) | DevOps/AzureDevTestLabs.puml
DevOps | AzureLabServices </br> (Azure Lab Services) | ![AzureLabServices](dist/DevOps/AzureLabServices.png?raw=true) | ![AzureLabServices](dist/DevOps/AzureLabServices(m).png?raw=true) | DevOps/AzureLabServices.puml
DevOps | AzurePipelines </br> (Azure Pipelines) | ![AzurePipelines](dist/DevOps/AzurePipelines.png?raw=true) | ![AzurePipelines](dist/DevOps/AzurePipelines(m).png?raw=true) | DevOps/AzurePipelines.puml
DevOps | AzureRepos </br> (Azure Repos) | ![AzureRepos](dist/DevOps/AzureRepos.png?raw=true) | ![AzureRepos](dist/DevOps/AzureRepos(m).png?raw=true) | DevOps/AzureRepos.puml
DevOps | AzureTestPlans </br> (Azure Test Plans) | ![AzureTestPlans](dist/DevOps/AzureTestPlans.png?raw=true) | ![AzureTestPlans](dist/DevOps/AzureTestPlans(m).png?raw=true) | DevOps/AzureTestPlans.puml
**General** | | | | **General/all.puml**
General | Azure </br> (Azure) | ![Azure](dist/General/Azure.png?raw=true) | ![Azure](dist/General/Azure(m).png?raw=true) | General/Azure.puml
**Identity** | | | | **Identity/all.puml**
Identity | AzureActiveDirectory </br> (Azure Active Directory) | ![AzureActiveDirectory](dist/Identity/AzureActiveDirectory.png?raw=true) | ![AzureActiveDirectory](dist/Identity/AzureActiveDirectory(m).png?raw=true) | Identity/AzureActiveDirectory.puml
Identity | AzureActiveDirectoryB2C </br> (Azure Active Directory B2C) | ![AzureActiveDirectoryB2C](dist/Identity/AzureActiveDirectoryB2C.png?raw=true) | ![AzureActiveDirectoryB2C](dist/Identity/AzureActiveDirectoryB2C(m).png?raw=true) | Identity/AzureActiveDirectoryB2C.puml
Identity | AzureActiveDirectoryDomainServices </br> (Azure Active Directory Domain Services) | ![AzureActiveDirectoryDomainServices](dist/Identity/AzureActiveDirectoryDomainServices.png?raw=true) | ![AzureActiveDirectoryDomainServices](dist/Identity/AzureActiveDirectoryDomainServices(m).png?raw=true) | Identity/AzureActiveDirectoryDomainServices.puml
**Integration** | | | | **Integration/all.puml**
Integration | AzureEventGrid </br> (Azure Event Grid) | ![AzureEventGrid](dist/Integration/AzureEventGrid.png?raw=true) | ![AzureEventGrid](dist/Integration/AzureEventGrid(m).png?raw=true) | Integration/AzureEventGrid.puml
Integration | AzureLogicApps </br> (Azure Logic Apps) | ![AzureLogicApps](dist/Integration/AzureLogicApps.png?raw=true) | ![AzureLogicApps](dist/Integration/AzureLogicApps(m).png?raw=true) | Integration/AzureLogicApps.puml
Integration | AzureServiceBus </br> (Azure Service Bus) | ![AzureServiceBus](dist/Integration/AzureServiceBus.png?raw=true) | ![AzureServiceBus](dist/Integration/AzureServiceBus(m).png?raw=true) | Integration/AzureServiceBus.puml
**InternetOfThings** | | | | **InternetOfThings/all.puml**
InternetOfThings | AzureDigitalTwins </br> (Azure Digital Twins) | ![AzureDigitalTwins](dist/InternetOfThings/AzureDigitalTwins.png?raw=true) | ![AzureDigitalTwins](dist/InternetOfThings/AzureDigitalTwins(m).png?raw=true) | InternetOfThings/AzureDigitalTwins.puml
InternetOfThings | AzureIoTCentral </br> (Azure IoT Central) | ![AzureIoTCentral](dist/InternetOfThings/AzureIoTCentral.png?raw=true) | ![AzureIoTCentral](dist/InternetOfThings/AzureIoTCentral(m).png?raw=true) | InternetOfThings/AzureIoTCentral.puml
InternetOfThings | AzureIoTEdge </br> (Azure IoT Edge) | ![AzureIoTEdge](dist/InternetOfThings/AzureIoTEdge.png?raw=true) | ![AzureIoTEdge](dist/InternetOfThings/AzureIoTEdge(m).png?raw=true) | InternetOfThings/AzureIoTEdge.puml
InternetOfThings | AzureIoTHub </br> (Azure IoT Hub) | ![AzureIoTHub](dist/InternetOfThings/AzureIoTHub.png?raw=true) | ![AzureIoTHub](dist/InternetOfThings/AzureIoTHub(m).png?raw=true) | InternetOfThings/AzureIoTHub.puml
InternetOfThings | AzureMaps </br> (Azure Maps) | ![AzureMaps](dist/InternetOfThings/AzureMaps.png?raw=true) | ![AzureMaps](dist/InternetOfThings/AzureMaps(m).png?raw=true) | InternetOfThings/AzureMaps.puml
InternetOfThings | AzureTimeSeriesInsights </br> (Azure Time Series Insights) | ![AzureTimeSeriesInsights](dist/InternetOfThings/AzureTimeSeriesInsights.png?raw=true) | ![AzureTimeSeriesInsights](dist/InternetOfThings/AzureTimeSeriesInsights(m).png?raw=true) | InternetOfThings/AzureTimeSeriesInsights.puml
**Management** | | | | **Management/all.puml**
Management | AzureAutomation </br> (Azure Automation) | ![AzureAutomation](dist/Management/AzureAutomation.png?raw=true) | ![AzureAutomation](dist/Management/AzureAutomation(m).png?raw=true) | Management/AzureAutomation.puml
Management | AzureBackup </br> (Azure Backup) | ![AzureBackup](dist/Management/AzureBackup.png?raw=true) | ![AzureBackup](dist/Management/AzureBackup(m).png?raw=true) | Management/AzureBackup.puml
Management | AzureBlueprints </br> (Azure Blueprints) | ![AzureBlueprints](dist/Management/AzureBlueprints.png?raw=true) | ![AzureBlueprints](dist/Management/AzureBlueprints(m).png?raw=true) | Management/AzureBlueprints.puml
Management | AzureLogAnalytics </br> (Azure Log Analytics) | ![AzureLogAnalytics](dist/Management/AzureLogAnalytics.png?raw=true) | ![AzureLogAnalytics](dist/Management/AzureLogAnalytics(m).png?raw=true) | Management/AzureLogAnalytics.puml
Management | AzureManagedApplications </br> (Azure Managed Applications) | ![AzureManagedApplications](dist/Management/AzureManagedApplications.png?raw=true) | ![AzureManagedApplications](dist/Management/AzureManagedApplications(m).png?raw=true) | Management/AzureManagedApplications.puml
Management | AzureManagementGroups </br> (Azure Management Groups) | ![AzureManagementGroups](dist/Management/AzureManagementGroups.png?raw=true) | ![AzureManagementGroups](dist/Management/AzureManagementGroups(m).png?raw=true) | Management/AzureManagementGroups.puml
Management | AzureMonitor </br> (Azure Monitor) | ![AzureMonitor](dist/Management/AzureMonitor.png?raw=true) | ![AzureMonitor](dist/Management/AzureMonitor(m).png?raw=true) | Management/AzureMonitor.puml
Management | AzurePolicy </br> (Azure Policy) | ![AzurePolicy](dist/Management/AzurePolicy.png?raw=true) | ![AzurePolicy](dist/Management/AzurePolicy(m).png?raw=true) | Management/AzurePolicy.puml
Management | AzureResourceGroups </br> (Azure Resource Groups) | ![AzureResourceGroups](dist/Management/AzureResourceGroups.png?raw=true) | ![AzureResourceGroups](dist/Management/AzureResourceGroups(m).png?raw=true) | Management/AzureResourceGroups.puml
Management | AzureScheduler </br> (Azure Scheduler) | ![AzureScheduler](dist/Management/AzureScheduler.png?raw=true) | ![AzureScheduler](dist/Management/AzureScheduler(m).png?raw=true) | Management/AzureScheduler.puml
Management | AzureSiteRecovery </br> (Azure Site Recovery) | ![AzureSiteRecovery](dist/Management/AzureSiteRecovery.png?raw=true) | ![AzureSiteRecovery](dist/Management/AzureSiteRecovery(m).png?raw=true) | Management/AzureSiteRecovery.puml
Management | AzureSubscription </br> (Azure Subscription) | ![AzureSubscription](dist/Management/AzureSubscription.png?raw=true) | ![AzureSubscription](dist/Management/AzureSubscription(m).png?raw=true) | Management/AzureSubscription.puml
**Media** | | | | **Media/all.puml**
Media | AzureMediaServices </br> (Azure Media Services) | ![AzureMediaServices](dist/Media/AzureMediaServices.png?raw=true) | ![AzureMediaServices](dist/Media/AzureMediaServices(m).png?raw=true) | Media/AzureMediaServices.puml
**Mobile** | | | | **Mobile/all.puml**
Mobile | AzureMobileApp </br> (Azure Mobile App) | ![AzureMobileApp](dist/Mobile/AzureMobileApp.png?raw=true) | ![AzureMobileApp](dist/Mobile/AzureMobileApp(m).png?raw=true) | Mobile/AzureMobileApp.puml
Mobile | AzureNotificationHubs </br> (Azure Notification Hubs) | ![AzureNotificationHubs](dist/Mobile/AzureNotificationHubs.png?raw=true) | ![AzureNotificationHubs](dist/Mobile/AzureNotificationHubs(m).png?raw=true) | Mobile/AzureNotificationHubs.puml
**Networking** | | | | **Networking/all.puml**
Networking | AzureApplicationGateway </br> (Azure Application Gateway) | ![AzureApplicationGateway](dist/Networking/AzureApplicationGateway.png?raw=true) | ![AzureApplicationGateway](dist/Networking/AzureApplicationGateway(m).png?raw=true) | Networking/AzureApplicationGateway.puml
Networking | AzureAzureDDoSProtection </br> (Azure Azure DDoS Protection) | ![AzureAzureDDoSProtection](dist/Networking/AzureAzureDDoSProtection.png?raw=true) | ![AzureAzureDDoSProtection](dist/Networking/AzureAzureDDoSProtection(m).png?raw=true) | Networking/AzureAzureDDoSProtection.puml
Networking | AzureDNS </br> (Azure DNS) | ![AzureDNS](dist/Networking/AzureDNS.png?raw=true) | ![AzureDNS](dist/Networking/AzureDNS(m).png?raw=true) | Networking/AzureDNS.puml
Networking | AzureExpressRoute </br> (Azure Express Route) | ![AzureExpressRoute](dist/Networking/AzureExpressRoute.png?raw=true) | ![AzureExpressRoute](dist/Networking/AzureExpressRoute(m).png?raw=true) | Networking/AzureExpressRoute.puml
Networking | AzureFrontDoorService </br> (Azure Front Door Service) | ![AzureFrontDoorService](dist/Networking/AzureFrontDoorService.png?raw=true) | ![AzureFrontDoorService](dist/Networking/AzureFrontDoorService(m).png?raw=true) | Networking/AzureFrontDoorService.puml
Networking | AzureLoadBalancer </br> (Azure Load Balancer) | ![AzureLoadBalancer](dist/Networking/AzureLoadBalancer.png?raw=true) | ![AzureLoadBalancer](dist/Networking/AzureLoadBalancer(m).png?raw=true) | Networking/AzureLoadBalancer.puml
Networking | AzureTrafficManager </br> (Azure Traffic Manager) | ![AzureTrafficManager](dist/Networking/AzureTrafficManager.png?raw=true) | ![AzureTrafficManager](dist/Networking/AzureTrafficManager(m).png?raw=true) | Networking/AzureTrafficManager.puml
Networking | AzureVirtualNetwork </br> (Azure Virtual Network) | ![AzureVirtualNetwork](dist/Networking/AzureVirtualNetwork.png?raw=true) | ![AzureVirtualNetwork](dist/Networking/AzureVirtualNetwork(m).png?raw=true) | Networking/AzureVirtualNetwork.puml
Networking | AzureVirtualWAN </br> (Azure Virtual WAN) | ![AzureVirtualWAN](dist/Networking/AzureVirtualWAN.png?raw=true) | ![AzureVirtualWAN](dist/Networking/AzureVirtualWAN(m).png?raw=true) | Networking/AzureVirtualWAN.puml
Networking | AzureVPNGateway </br> (Azure VPN Gateway) | ![AzureVPNGateway](dist/Networking/AzureVPNGateway.png?raw=true) | ![AzureVPNGateway](dist/Networking/AzureVPNGateway(m).png?raw=true) | Networking/AzureVPNGateway.puml
**Security** | | | | **Security/all.puml**
Security | AzureKeyVault </br> (Azure Key Vault) | ![AzureKeyVault](dist/Security/AzureKeyVault.png?raw=true) | ![AzureKeyVault](dist/Security/AzureKeyVault(m).png?raw=true) | Security/AzureKeyVault.puml
Security | AzureSentinel </br> (Azure Sentinel) | ![AzureSentinel](dist/Security/AzureSentinel.png?raw=true) | ![AzureSentinel](dist/Security/AzureSentinel(m).png?raw=true) | Security/AzureSentinel.puml
**Storage** | | | | **Storage/all.puml**
Storage | AzureBlobStorage </br> (Azure Blob Storage) | ![AzureBlobStorage](dist/Storage/AzureBlobStorage.png?raw=true) | ![AzureBlobStorage](dist/Storage/AzureBlobStorage(m).png?raw=true) | Storage/AzureBlobStorage.puml
Storage | AzureDataBox </br> (Azure Data Box) | ![AzureDataBox](dist/Storage/AzureDataBox.png?raw=true) | ![AzureDataBox](dist/Storage/AzureDataBox(m).png?raw=true) | Storage/AzureDataBox.puml
Storage | AzureDataLakeStorage </br> (Azure Data Lake Storage) | ![AzureDataLakeStorage](dist/Storage/AzureDataLakeStorage.png?raw=true) | ![AzureDataLakeStorage](dist/Storage/AzureDataLakeStorage(m).png?raw=true) | Storage/AzureDataLakeStorage.puml
Storage | AzureDiskStorage </br> (Azure Disk Storage) | ![AzureDiskStorage](dist/Storage/AzureDiskStorage.png?raw=true) | ![AzureDiskStorage](dist/Storage/AzureDiskStorage(m).png?raw=true) | Storage/AzureDiskStorage.puml
Storage | AzureFileStorage </br> (Azure File Storage) | ![AzureFileStorage](dist/Storage/AzureFileStorage.png?raw=true) | ![AzureFileStorage](dist/Storage/AzureFileStorage(m).png?raw=true) | Storage/AzureFileStorage.puml
Storage | AzureManagedDisks </br> (Azure Managed Disks) | ![AzureManagedDisks](dist/Storage/AzureManagedDisks.png?raw=true) | ![AzureManagedDisks](dist/Storage/AzureManagedDisks(m).png?raw=true) | Storage/AzureManagedDisks.puml
Storage | AzureNetAppFiles </br> (Azure Net App Files) | ![AzureNetAppFiles](dist/Storage/AzureNetAppFiles.png?raw=true) | ![AzureNetAppFiles](dist/Storage/AzureNetAppFiles(m).png?raw=true) | Storage/AzureNetAppFiles.puml
Storage | AzureQueueStorage </br> (Azure Queue Storage) | ![AzureQueueStorage](dist/Storage/AzureQueueStorage.png?raw=true) | ![AzureQueueStorage](dist/Storage/AzureQueueStorage(m).png?raw=true) | Storage/AzureQueueStorage.puml
Storage | AzureStorage </br> (Azure Storage) | ![AzureStorage](dist/Storage/AzureStorage.png?raw=true) | ![AzureStorage](dist/Storage/AzureStorage(m).png?raw=true) | Storage/AzureStorage.puml
Storage | AzureStorSimple </br> (Azure Stor Simple) | ![AzureStorSimple](dist/Storage/AzureStorSimple.png?raw=true) | ![AzureStorSimple](dist/Storage/AzureStorSimple(m).png?raw=true) | Storage/AzureStorSimple.puml
**Web** | | | | **Web/all.puml**
Web | AzureAPIManagement </br> (Azure API Management) | ![AzureAPIManagement](dist/Web/AzureAPIManagement.png?raw=true) | ![AzureAPIManagement](dist/Web/AzureAPIManagement(m).png?raw=true) | Web/AzureAPIManagement.puml
Web | AzureCDN </br> (Azure CDN) | ![AzureCDN](dist/Web/AzureCDN.png?raw=true) | ![AzureCDN](dist/Web/AzureCDN(m).png?raw=true) | Web/AzureCDN.puml
Web | AzureSearch </br> (Azure Search) | ![AzureSearch](dist/Web/AzureSearch.png?raw=true) | ![AzureSearch](dist/Web/AzureSearch(m).png?raw=true) | Web/AzureSearch.puml
Web | AzureSignalRService </br> (Azure SignalR Service) | ![AzureSignalRService](dist/Web/AzureSignalRService.png?raw=true) | ![AzureSignalRService](dist/Web/AzureSignalRService(m).png?raw=true) | Web/AzureSignalRService.puml
Web | AzureWebApp </br> (Azure Web App) | ![AzureWebApp](dist/Web/AzureWebApp.png?raw=true) | ![AzureWebApp](dist/Web/AzureWebApp(m).png?raw=true) | Web/AzureWebApp.puml
