# csharp_wsMonitorizaInternet

**Servicio Windows en C# para la monitorización de la conexión a internet.**

### Funcionamiento del servicio

>Este servicio una vez instalado en el equipo Windows realiza 'pings' al servidor de DNS de Google 8.8.8.8 cada 30 segundos y registra en el log de eventos del sistema los pings fallidos.

### Recompilación e instalación del servicio

Pasos para recompilar en Visual Studio e instalar el servicio de nuevo:

1. "detener el servicio" . . . . . . . . . . . . . . . . . . . **net stop wsMonitorizaInternet**
2. "desinstalar el servicio" . . . . . . . . . . . . . . . . **installutil /u wsMonitorizaInternet.exe**
3. *RECOMPILAR EL PROYECTO/ SOLUCION en VisualStudio*
4. "instalar el servicio" . . . . . . . . . . . . . . . . . . . **installutil wsMonitorizaInternet.exe**
5. "iniciar el servicio" . . . . . . . . . . . . . . . . . . . . **net start wsMonitorizaInternet**

#### y tenga en cuenta que . . .

**1º Los pasos 1 y 5 requieren acceso a la consola como administrador**

    Ejecute como "admin" el siguiente Command Prompt
>Inicio > Visual Studio 2019 > **Developer Command Prompt**

**2º Los pasos 2 y 4 requieren estar en la ruta del Servicio compilado o bien indicar la ruta al ejecutable.**
  
    ej.
    ----
    C:\>cd \Users\miusuario\source\repos\csharp_wsMonitorizaInternet\wsMonitorizaInternet\bin\Debug>
    C:\>installutil wsMonitorizaInternet.exe

## Herramientas

* **services.msc** . . . . . . . . . . . *Listado de servicios*
* **eventvwr.msc** . . . . . . . . . . *Consulta de eventos/logs*
