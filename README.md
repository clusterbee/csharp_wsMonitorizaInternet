# csharp_wsMonitorizaInternet
----
Servicio Windows en C# para la monitorización de la conexión a internet.

## Funcionamiento del servicio

>Este servicio una vez instalado en el equipo Windows va realizando 'pings' al servidor de DNS de Google 8.8.8.8 cada 30 segundos y va registrando en el log de eventos del sistema aquellos pings que no han obtenido una respuesta satisfactoria.

>Al servicio se le ha llamado :  wsMonitorizaInternet
pero desde el código fuente puede cambiarlo a uno de su gusto, así como el equipo al que hacer ping en lugar de 8.8.8.8 y el tiempo de espera entre pings en vez de 30 segundos.

>Una mejora sería poder suministrar estos parámetros (equipo remoto e intervalo entre pings) o bien en el momento de la instalación del servicio como parámetros al comando o bien leyendo los mismos desde un fichero de configuración.

## Recompilación e instalación del servicio

Pasos para recompilar en Visual Studio e instalar el servicio de nuevo:

	1. net stop wsMonitorizaInternet				     --> DETENER SERVICIO si lo tuviéramos corriendo.
	2. installutil /u wsMonitorizaInternet.exe				--> DESINSTALAR SERVICIO si lo tuviéramos instalado de antes.
	3. RECOMPILAR EL PROYECTO/ SOLUCION en VisualStudio
	4. installutil wsMonitorizaInternet.exe					  --> INSTALAR SERVICIO
	5. net start wsMonitorizaInternet						      --> INICIAR SERVICIO

# NOTAS
Los pasos 1 y 5 requieren acceso a la consola como administrador (Inicio > Visual Studio 2019 > Developer Command Prompt) <<<< Ejecutar como "admin" desde el menu contextual.

Los pasos 2 y 4 requieren estar en la ruta del Servicio compilado o indicar bien indica la ruta a ejecutable
  
ej.  C:\>cd \Users\miusuario\source\repos\csharp_wsMonitorizaInternet\wsMonitorizaInternet\bin\Debug>
C:\>installutil wsMonitorizaInternet.exe

# Herramientas
>services.msc	<<< Listado de servicios
>eventvwr.msc	<<< Consulta de eventos/logs
  
