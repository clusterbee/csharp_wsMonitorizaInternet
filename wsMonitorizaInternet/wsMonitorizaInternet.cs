/*
 * Información consultada:
 * 
 * https://docs.microsoft.com/es-es/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer
 * 
 * https://docs.microsoft.com/es-es/dotnet/api/system.net.networkinformation.ping.send?view=netframework-4.8
 *
 */

using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace wsMonitorizaInternet
{
    public partial class wsMonitorizaInternet : ServiceBase
    {
        public wsMonitorizaInternet(string[] args)
        {
            InitializeComponent();

            string eventSourceName = "MonitorizaInternetService";
            string logName = "MonitorizaInternetLog";

            if (args.Length > 0) eventSourceName = args[0];
            if (args.Length > 1) logName = args[1];

            eventLog1 = new EventLog();

            if (!EventLog.SourceExists(eventSourceName))
                EventLog.CreateEventSource(eventSourceName, logName);

            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // TODO: Código para la monitorización de la actividad del servicio.

            string reply = RealizarPing(HOST_DESTINO, PING_TIMEOUT);

            if (reply.ToLower() != "success")
                eventLog1.WriteEntry($"Sin conexión a internet.\n\n{reply}", EventLogEntryType.Error, eventId++);
        }

        protected override void OnStart(string[] args)
        {
            // Actualiza el estado del servicio a... "Inicio Pendiente"
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = EstadoServicio.SERVICIO_INICIO_PENDIENTE;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Registrando evento de inicio del servicio de monitorización
            eventLog1.WriteEntry("Servicio de monitorización de internet -> INICIADO.");

            // Configura un temporizador para activarse cada 10 segundos.
            Timer timer = new Timer();
            timer.Interval = TIMER_INTERVAL;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Actualiza el estado del servicio a "Ejecutándose"
            serviceStatus.dwCurrentState = EstadoServicio.SERVICIO_CORRIENDO;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            // Actualiza el estado del servicio a... "Detención Pendiente"
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = EstadoServicio.SERVICIO_PARO_PENDIENTE;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Registrando evento de detención del servicio de monitorización
            eventLog1.WriteEntry("Servicio de monitorización de internet -> DETENIDO.");

            // Actualiza el estado del servicio a "Detenido"
            serviceStatus.dwCurrentState = EstadoServicio.SERVICIO_DETENIDO;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private int eventId = 1;

        private const int PING_TIMEOUT = 5000;          //  5 segundos
        private const int TIMER_INTERVAL = 30000;       // 30 segundos
        private const string HOST_DESTINO = "8.8.8.8";  // Servidor DNS principal de Google
    }

    public partial class wsMonitorizaInternet : ServiceBase
    {
        /*
         * Implementación del estado pendiente del servicio
         */
        public enum EstadoServicio
        {
            SERVICIO_DETENIDO               = 0x00000001,
            SERVICIO_INICIO_PENDIENTE       = 0x00000002,
            SERVICIO_PARO_PENDIENTE         = 0x00000003,
            SERVICIO_CORRIENDO              = 0x00000004,
            SERVICIO_CONTINUAR_PENDIENTE    = 0x00000005,
            SERVICIO_PAUSA_PENDIENTE        = 0x00000006,
            SERVICIO_PAUSADO                = 0x00000007
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public EstadoServicio dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
    }

    public partial class wsMonitorizaInternet : ServiceBase
    {
        /// <summary>
        /// Realizar un ping hacia un equipo en internet.
        /// </summary>
        /// <param name="destino">Una dirección IP o un nombre de equipo al que se hará ping.</param>
        /// <param name="milisecTimeout">Timeout del ping en milisegundos.</param>
        /// <returns></returns>
        public static string RealizarPing(string destino, int milisecTimeout)
        {
            Ping miPing = new Ping();

            // Creación de un buffer de 32 bytes para la transmisión de información.
            string datos = "En un lugar de la Mancha de cuyo";
            byte[] buffer = Encoding.ASCII.GetBytes(datos);

            // Opciones para la transmisión:
            //      Los datos pueden pasar a través de 64 pasarelas o enrutadores
            //      antes de ser destruidos.
            //      Los datos no viajarán fragmentados.

            PingOptions opciones = new PingOptions(64, true);

            // Haciendo ping.
            PingReply respuesta = miPing.Send(destino, milisecTimeout, buffer, opciones);

            // Devolvemos una cadena con el resultado del estado del ping.
            return respuesta.Status.ToString();
        }
    }
}
