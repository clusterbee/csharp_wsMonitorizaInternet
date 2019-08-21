using System.ServiceProcess;

namespace wsMonitorizaInternet
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada a la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new wsMonitorizaInternet(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
