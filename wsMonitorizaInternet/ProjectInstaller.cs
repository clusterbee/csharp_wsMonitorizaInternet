using System.ComponentModel;
using System.Configuration.Install;

namespace wsMonitorizaInternet
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ServiceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}