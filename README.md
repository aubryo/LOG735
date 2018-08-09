L'ordinateur doit avoir .Net Framework 4.6 d'installé

Pour faire fonctionner l’application web dans un état minimal, il faut un serveur minimum. 
Si l’application est roulée avec visual studio, il faut configurer le démarrage d’application en multiplie projet. 
Cela permet de choisir l’application web Log735Schedule comme projet de démarrage et le projet SignalRPrivateRoomServices2
comme projet de démarrage également. Le ports 8088 doit être libre pour l'exécution du service.

Une seconde approche est la création d’un Windows Service sur l’ordinateur. 
Choisir le projet SignalRPrivateRoomServices pour l’installation du Windows Service. 
Pour installer un windows services suivre les instructions sur le lien suivant : 
https://docs.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services. 
Le port 8089 doit être libre pour l’exécution du service. 
Une fois l’installation terminée et le Windows Service démarré, utiliser visual studio pour partir l’application Web Log735Schedule.

Pour utiliser les deux serveurs du projet. Les deux configurations ci-dessus doivent être réalisées.
