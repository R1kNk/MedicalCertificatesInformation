using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalCertificatesInformation.Startup))]
namespace MedicalCertificatesInformation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
