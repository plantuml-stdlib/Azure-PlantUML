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

> Tipp: You can also use the colored and monochrom PNG images outside of PlantUML, e.g. for documents or presentations.
> If the size of the generated images is not enough for you, take a look at [how you can build Azure-PlantUML on your own](scripts/README.md).

## All generated Azure symbols and PlantUML sprites

Category | Macro | <pre>Color</pre> | <pre>Mono </pre> | Url
  ---    |  ---  | :---:  | :---: | ---
**Analytics** | | | | **Analytics/all.puml**
Analytics |AzureAnalysisServices | |![AzureAnalysisServices](dist/Analytics/AzureAnalysisServices(m).png?raw=true) |Analytics/AzureAnalysisServices.puml
Analytics |AzureDatabricks |![AzureDatabricks](dist/Analytics/AzureDatabricks.png?raw=true) |![AzureDatabricks](dist/Analytics/AzureDatabricks(m).png?raw=true) |Analytics/AzureDatabricks.puml
Analytics |AzureDataCatalog |![AzureDataCatalog](dist/Analytics/AzureDataCatalog.png?raw=true) |![AzureDataCatalog](dist/Analytics/AzureDataCatalog(m).png?raw=true) |Analytics/AzureDataCatalog.puml
Analytics |AzureDataLakeAnalytics |![AzureDataLakeAnalytics](dist/Analytics/AzureDataLakeAnalytics.png?raw=true) |![AzureDataLakeAnalytics](dist/Analytics/AzureDataLakeAnalytics(m).png?raw=true) |Analytics/AzureDataLakeAnalytics.puml
Analytics |AzureEventHub |![AzureEventHub](dist/Analytics/AzureEventHub.png?raw=true) |![AzureEventHub](dist/Analytics/AzureEventHub(m).png?raw=true) |Analytics/AzureEventHub.puml
Analytics |AzureHDInsight |![AzureHDInsight](dist/Analytics/AzureHDInsight.png?raw=true) |![AzureHDInsight](dist/Analytics/AzureHDInsight(m).png?raw=true) |Analytics/AzureHDInsight.puml
Analytics |AzureStreamAnalytics |![AzureStreamAnalytics](dist/Analytics/AzureStreamAnalytics.png?raw=true) |![AzureStreamAnalytics](dist/Analytics/AzureStreamAnalytics(m).png?raw=true) |Analytics/AzureStreamAnalytics.puml
**Compute** | | | | **Compute/all.puml**
Compute |AzureAppService |![AzureAppService](dist/Compute/AzureAppService.png?raw=true) |![AzureAppService](dist/Compute/AzureAppService(m).png?raw=true) |Compute/AzureAppService.puml
Compute |AzureBatch |![AzureBatch](dist/Compute/AzureBatch.png?raw=true) |![AzureBatch](dist/Compute/AzureBatch(m).png?raw=true) |Compute/AzureBatch.puml
Compute |AzureBatchAI |![AzureBatchAI](dist/Compute/AzureBatchAI.png?raw=true) |![AzureBatchAI](dist/Compute/AzureBatchAI(m).png?raw=true) |Compute/AzureBatchAI.puml
Compute |AzureFunction |![AzureFunction](dist/Compute/AzureFunction.png?raw=true) |![AzureFunction](dist/Compute/AzureFunction(m).png?raw=true) |Compute/AzureFunction.puml
Compute |AzureServiceFabric |![AzureServiceFabric](dist/Compute/AzureServiceFabric.png?raw=true) |![AzureServiceFabric](dist/Compute/AzureServiceFabric(m).png?raw=true) |Compute/AzureServiceFabric.puml
Compute |AzureVirtualMachine |![AzureVirtualMachine](dist/Compute/AzureVirtualMachine.png?raw=true) |![AzureVirtualMachine](dist/Compute/AzureVirtualMachine(m).png?raw=true) |Compute/AzureVirtualMachine.puml
Compute |AzureVirtualMachineScaleSet |![AzureVirtualMachineScaleSet](dist/Compute/AzureVirtualMachineScaleSet.png?raw=true) |![AzureVirtualMachineScaleSet](dist/Compute/AzureVirtualMachineScaleSet(m).png?raw=true) |Compute/AzureVirtualMachineScaleSet.puml
**Containers** | | | | **Containers/all.puml**
Containers |AzureContainerInstance |![AzureContainerInstance](dist/Containers/AzureContainerInstance.png?raw=true) |![AzureContainerInstance](dist/Containers/AzureContainerInstance(m).png?raw=true) |Containers/AzureContainerInstance.puml
Containers |AzureContainerRegistry |![AzureContainerRegistry](dist/Containers/AzureContainerRegistry.png?raw=true) |![AzureContainerRegistry](dist/Containers/AzureContainerRegistry(m).png?raw=true) |Containers/AzureContainerRegistry.puml
Containers |AzureKubernetesService |![AzureKubernetesService](dist/Containers/AzureKubernetesService.png?raw=true) |![AzureKubernetesService](dist/Containers/AzureKubernetesService(m).png?raw=true) |Containers/AzureKubernetesService.puml
Containers |AzureWebAppForContainers |![AzureWebAppForContainers](dist/Containers/AzureWebAppForContainers.png?raw=true) |![AzureWebAppForContainers](dist/Containers/AzureWebAppForContainers(m).png?raw=true) |Containers/AzureWebAppForContainers.puml
**Databases** | | | | **Databases/all.puml**
Databases |AzureCosmosDb |![AzureCosmosDb](dist/Databases/AzureCosmosDb.png?raw=true) |![AzureCosmosDb](dist/Databases/AzureCosmosDb(m).png?raw=true) |Databases/AzureCosmosDb.puml
Databases |AzureDatabaseForMySQL |![AzureDatabaseForMySQL](dist/Databases/AzureDatabaseForMySQL.png?raw=true) |![AzureDatabaseForMySQL](dist/Databases/AzureDatabaseForMySQL(m).png?raw=true) |Databases/AzureDatabaseForMySQL.puml
Databases |AzureDatabaseForPostgreSQL |![AzureDatabaseForPostgreSQL](dist/Databases/AzureDatabaseForPostgreSQL.png?raw=true) |![AzureDatabaseForPostgreSQL](dist/Databases/AzureDatabaseForPostgreSQL(m).png?raw=true) |Databases/AzureDatabaseForPostgreSQL.puml
Databases |AzureDataFactory |![AzureDataFactory](dist/Databases/AzureDataFactory.png?raw=true) |![AzureDataFactory](dist/Databases/AzureDataFactory(m).png?raw=true) |Databases/AzureDataFactory.puml
Databases |AzureRedisCache |![AzureRedisCache](dist/Databases/AzureRedisCache.png?raw=true) |![AzureRedisCache](dist/Databases/AzureRedisCache(m).png?raw=true) |Databases/AzureRedisCache.puml
Databases |AzureSqlDatabase |![AzureSqlDatabase](dist/Databases/AzureSqlDatabase.png?raw=true) |![AzureSqlDatabase](dist/Databases/AzureSqlDatabase(m).png?raw=true) |Databases/AzureSqlDatabase.puml
Databases |AzureSqlDataWarehouse |![AzureSqlDataWarehouse](dist/Databases/AzureSqlDataWarehouse.png?raw=true) |![AzureSqlDataWarehouse](dist/Databases/AzureSqlDataWarehouse(m).png?raw=true) |Databases/AzureSqlDataWarehouse.puml
Databases |AzureSqlStretchDatabase |![AzureSqlStretchDatabase](dist/Databases/AzureSqlStretchDatabase.png?raw=true) |![AzureSqlStretchDatabase](dist/Databases/AzureSqlStretchDatabase(m).png?raw=true) |Databases/AzureSqlStretchDatabase.puml
**DevOps** | | | | **DevOps/all.puml**
DevOps |AzureApplicationInsights |![AzureApplicationInsights](dist/DevOps/AzureApplicationInsights.png?raw=true) |![AzureApplicationInsights](dist/DevOps/AzureApplicationInsights(m).png?raw=true) |DevOps/AzureApplicationInsights.puml
DevOps |AzureArtifacts |![AzureArtifacts](dist/DevOps/AzureArtifacts.png?raw=true) |![AzureArtifacts](dist/DevOps/AzureArtifacts(m).png?raw=true) |DevOps/AzureArtifacts.puml
DevOps |AzureBoards |![AzureBoards](dist/DevOps/AzureBoards.png?raw=true) |![AzureBoards](dist/DevOps/AzureBoards(m).png?raw=true) |DevOps/AzureBoards.puml
DevOps |AzureDevOps |![AzureDevOps](dist/DevOps/AzureDevOps.png?raw=true) |![AzureDevOps](dist/DevOps/AzureDevOps(m).png?raw=true) |DevOps/AzureDevOps.puml
DevOps |AzureDevTestLabs |![AzureDevTestLabs](dist/DevOps/AzureDevTestLabs.png?raw=true) |![AzureDevTestLabs](dist/DevOps/AzureDevTestLabs(m).png?raw=true) |DevOps/AzureDevTestLabs.puml
DevOps |AzureLabServices |![AzureLabServices](dist/DevOps/AzureLabServices.png?raw=true) |![AzureLabServices](dist/DevOps/AzureLabServices(m).png?raw=true) |DevOps/AzureLabServices.puml
DevOps |AzurePipelines |![AzurePipelines](dist/DevOps/AzurePipelines.png?raw=true) |![AzurePipelines](dist/DevOps/AzurePipelines(m).png?raw=true) |DevOps/AzurePipelines.puml
DevOps |AzureRepos |![AzureRepos](dist/DevOps/AzureRepos.png?raw=true) |![AzureRepos](dist/DevOps/AzureRepos(m).png?raw=true) |DevOps/AzureRepos.puml
DevOps |AzureTestPlans |![AzureTestPlans](dist/DevOps/AzureTestPlans.png?raw=true) |![AzureTestPlans](dist/DevOps/AzureTestPlans(m).png?raw=true) |DevOps/AzureTestPlans.puml
**Identity** | | | | **Identity/all.puml**
Identity |AzureActiveDirectory |![AzureActiveDirectory](dist/Identity/AzureActiveDirectory.png?raw=true) |![AzureActiveDirectory](dist/Identity/AzureActiveDirectory(m).png?raw=true) |Identity/AzureActiveDirectory.puml
Identity |AzureActiveDirectoryB2C |![AzureActiveDirectoryB2C](dist/Identity/AzureActiveDirectoryB2C.png?raw=true) |![AzureActiveDirectoryB2C](dist/Identity/AzureActiveDirectoryB2C(m).png?raw=true) |Identity/AzureActiveDirectoryB2C.puml
Identity |AzureActiveDirectoryDomainServices |![AzureActiveDirectoryDomainServices](dist/Identity/AzureActiveDirectoryDomainServices.png?raw=true) |![AzureActiveDirectoryDomainServices](dist/Identity/AzureActiveDirectoryDomainServices(m).png?raw=true) |Identity/AzureActiveDirectoryDomainServices.puml
**Integration** | | | | **Integration/all.puml**
Integration |AzureEventGrid |![AzureEventGrid](dist/Integration/AzureEventGrid.png?raw=true) |![AzureEventGrid](dist/Integration/AzureEventGrid(m).png?raw=true) |Integration/AzureEventGrid.puml
Integration |AzureLogicApps |![AzureLogicApps](dist/Integration/AzureLogicApps.png?raw=true) |![AzureLogicApps](dist/Integration/AzureLogicApps(m).png?raw=true) |Integration/AzureLogicApps.puml
Integration |AzureServiceBus |![AzureServiceBus](dist/Integration/AzureServiceBus.png?raw=true) |![AzureServiceBus](dist/Integration/AzureServiceBus(m).png?raw=true) |Integration/AzureServiceBus.puml
**InternetOfThings** | | | | **InternetOfThings/all.puml**
InternetOfThings |AzureIoTCentral | |![AzureIoTCentral](dist/InternetOfThings/AzureIoTCentral(m).png?raw=true) |InternetOfThings/AzureIoTCentral.puml
InternetOfThings |AzureIoTEdge |![AzureIoTEdge](dist/InternetOfThings/AzureIoTEdge.png?raw=true) |![AzureIoTEdge](dist/InternetOfThings/AzureIoTEdge(m).png?raw=true) |InternetOfThings/AzureIoTEdge.puml
InternetOfThings |AzureIoTHub |![AzureIoTHub](dist/InternetOfThings/AzureIoTHub.png?raw=true) |![AzureIoTHub](dist/InternetOfThings/AzureIoTHub(m).png?raw=true) |InternetOfThings/AzureIoTHub.puml
InternetOfThings |AzureMaps |![AzureMaps](dist/InternetOfThings/AzureMaps.png?raw=true) |![AzureMaps](dist/InternetOfThings/AzureMaps(m).png?raw=true) |InternetOfThings/AzureMaps.puml
InternetOfThings |AzureTimeSeriesInsights |![AzureTimeSeriesInsights](dist/InternetOfThings/AzureTimeSeriesInsights.png?raw=true) |![AzureTimeSeriesInsights](dist/InternetOfThings/AzureTimeSeriesInsights(m).png?raw=true) |InternetOfThings/AzureTimeSeriesInsights.puml
**Management** | | | | **Management/all.puml**
Management |AzureAutomation |![AzureAutomation](dist/Management/AzureAutomation.png?raw=true) |![AzureAutomation](dist/Management/AzureAutomation(m).png?raw=true) |Management/AzureAutomation.puml
Management |AzureBackup |![AzureBackup](dist/Management/AzureBackup.png?raw=true) |![AzureBackup](dist/Management/AzureBackup(m).png?raw=true) |Management/AzureBackup.puml
Management |AzureLogAnalytics | |![AzureLogAnalytics](dist/Management/AzureLogAnalytics(m).png?raw=true) |Management/AzureLogAnalytics.puml
Management |AzureMonitor |![AzureMonitor](dist/Management/AzureMonitor.png?raw=true) |![AzureMonitor](dist/Management/AzureMonitor(m).png?raw=true) |Management/AzureMonitor.puml
Management |AzurePolicy |![AzurePolicy](dist/Management/AzurePolicy.png?raw=true) |![AzurePolicy](dist/Management/AzurePolicy(m).png?raw=true) |Management/AzurePolicy.puml
Management |AzureResourceManager | |![AzureResourceManager](dist/Management/AzureResourceManager(m).png?raw=true) |Management/AzureResourceManager.puml
Management |AzureScheduler |![AzureScheduler](dist/Management/AzureScheduler.png?raw=true) |![AzureScheduler](dist/Management/AzureScheduler(m).png?raw=true) |Management/AzureScheduler.puml
Management |AzureSiteRecovery |![AzureSiteRecovery](dist/Management/AzureSiteRecovery.png?raw=true) |![AzureSiteRecovery](dist/Management/AzureSiteRecovery(m).png?raw=true) |Management/AzureSiteRecovery.puml
**Media** | | | | **Media/all.puml**
Media |AzureMediaServices |![AzureMediaServices](dist/Media/AzureMediaServices.png?raw=true) |![AzureMediaServices](dist/Media/AzureMediaServices(m).png?raw=true) |Media/AzureMediaServices.puml
**Mobile** | | | | **Mobile/all.puml**
Mobile |AzureMobileApp |![AzureMobileApp](dist/Mobile/AzureMobileApp.png?raw=true) |![AzureMobileApp](dist/Mobile/AzureMobileApp(m).png?raw=true) |Mobile/AzureMobileApp.puml
Mobile |AzureNotificationHubs |![AzureNotificationHubs](dist/Mobile/AzureNotificationHubs.png?raw=true) |![AzureNotificationHubs](dist/Mobile/AzureNotificationHubs(m).png?raw=true) |Mobile/AzureNotificationHubs.puml
**Networking** | | | | **Networking/all.puml**
Networking |AzureApplicationGateway |![AzureApplicationGateway](dist/Networking/AzureApplicationGateway.png?raw=true) |![AzureApplicationGateway](dist/Networking/AzureApplicationGateway(m).png?raw=true) |Networking/AzureApplicationGateway.puml
Networking |AzureAzureDDoSProtection |![AzureAzureDDoSProtection](dist/Networking/AzureAzureDDoSProtection.png?raw=true) |![AzureAzureDDoSProtection](dist/Networking/AzureAzureDDoSProtection(m).png?raw=true) |Networking/AzureAzureDDoSProtection.puml
Networking |AzureDNS |![AzureDNS](dist/Networking/AzureDNS.png?raw=true) |![AzureDNS](dist/Networking/AzureDNS(m).png?raw=true) |Networking/AzureDNS.puml
Networking |AzureExpressRoute |![AzureExpressRoute](dist/Networking/AzureExpressRoute.png?raw=true) |![AzureExpressRoute](dist/Networking/AzureExpressRoute(m).png?raw=true) |Networking/AzureExpressRoute.puml
Networking |AzureLoadBalancer |![AzureLoadBalancer](dist/Networking/AzureLoadBalancer.png?raw=true) |![AzureLoadBalancer](dist/Networking/AzureLoadBalancer(m).png?raw=true) |Networking/AzureLoadBalancer.puml
Networking |AzureTrafficManager |![AzureTrafficManager](dist/Networking/AzureTrafficManager.png?raw=true) |![AzureTrafficManager](dist/Networking/AzureTrafficManager(m).png?raw=true) |Networking/AzureTrafficManager.puml
Networking |AzureVirtualNetwork |![AzureVirtualNetwork](dist/Networking/AzureVirtualNetwork.png?raw=true) |![AzureVirtualNetwork](dist/Networking/AzureVirtualNetwork(m).png?raw=true) |Networking/AzureVirtualNetwork.puml
Networking |AzureVirtualWAN |![AzureVirtualWAN](dist/Networking/AzureVirtualWAN.png?raw=true) |![AzureVirtualWAN](dist/Networking/AzureVirtualWAN(m).png?raw=true) |Networking/AzureVirtualWAN.puml
Networking |AzureVPNGateway |![AzureVPNGateway](dist/Networking/AzureVPNGateway.png?raw=true) |![AzureVPNGateway](dist/Networking/AzureVPNGateway(m).png?raw=true) |Networking/AzureVPNGateway.puml
**Security** | | | | **Security/all.puml**
Security |AzureKeyVault |![AzureKeyVault](dist/Security/AzureKeyVault.png?raw=true) |![AzureKeyVault](dist/Security/AzureKeyVault(m).png?raw=true) |Security/AzureKeyVault.puml
**Storage** | | | | **Storage/all.puml**
Storage |AzureAzureNetAppFiles |![AzureAzureNetAppFiles](dist/Storage/AzureAzureNetAppFiles.png?raw=true) |![AzureAzureNetAppFiles](dist/Storage/AzureAzureNetAppFiles(m).png?raw=true) |Storage/AzureAzureNetAppFiles.puml
Storage |AzureBlobStorage | |![AzureBlobStorage](dist/Storage/AzureBlobStorage(m).png?raw=true) |Storage/AzureBlobStorage.puml
Storage |AzureDataLakeStorage |![AzureDataLakeStorage](dist/Storage/AzureDataLakeStorage.png?raw=true) |![AzureDataLakeStorage](dist/Storage/AzureDataLakeStorage(m).png?raw=true) |Storage/AzureDataLakeStorage.puml
Storage |AzureDiskStorage |![AzureDiskStorage](dist/Storage/AzureDiskStorage.png?raw=true) |![AzureDiskStorage](dist/Storage/AzureDiskStorage(m).png?raw=true) |Storage/AzureDiskStorage.puml
Storage |AzureFileStorage | |![AzureFileStorage](dist/Storage/AzureFileStorage(m).png?raw=true) |Storage/AzureFileStorage.puml
Storage |AzureManagedDisks |![AzureManagedDisks](dist/Storage/AzureManagedDisks.png?raw=true) |![AzureManagedDisks](dist/Storage/AzureManagedDisks(m).png?raw=true) |Storage/AzureManagedDisks.puml
Storage |AzureQueueStorage | |![AzureQueueStorage](dist/Storage/AzureQueueStorage(m).png?raw=true) |Storage/AzureQueueStorage.puml
Storage |AzureStorage | |![AzureStorage](dist/Storage/AzureStorage(m).png?raw=true) |Storage/AzureStorage.puml
Storage |AzureStorSimple |![AzureStorSimple](dist/Storage/AzureStorSimple.png?raw=true) |![AzureStorSimple](dist/Storage/AzureStorSimple(m).png?raw=true) |Storage/AzureStorSimple.puml
**Web** | | | | **Web/all.puml**
Web |AzureAPIManagement |![AzureAPIManagement](dist/Web/AzureAPIManagement.png?raw=true) |![AzureAPIManagement](dist/Web/AzureAPIManagement(m).png?raw=true) |Web/AzureAPIManagement.puml
Web |AzureCDN |![AzureCDN](dist/Web/AzureCDN.png?raw=true) |![AzureCDN](dist/Web/AzureCDN(m).png?raw=true) |Web/AzureCDN.puml
Web |AzureSearch |![AzureSearch](dist/Web/AzureSearch.png?raw=true) |![AzureSearch](dist/Web/AzureSearch(m).png?raw=true) |Web/AzureSearch.puml
Web |AzureSignalRService |![AzureSignalRService](dist/Web/AzureSignalRService.png?raw=true) |![AzureSignalRService](dist/Web/AzureSignalRService(m).png?raw=true) |Web/AzureSignalRService.puml
Web |AzureWebApp |![AzureWebApp](dist/Web/AzureWebApp.png?raw=true) |![AzureWebApp](dist/Web/AzureWebApp(m).png?raw=true) |Web/AzureWebApp.puml