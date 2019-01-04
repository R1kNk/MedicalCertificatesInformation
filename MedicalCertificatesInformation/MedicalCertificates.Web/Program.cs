using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MedicalSertificates.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("ru-RU");
            }
            // If an exception occurs, we'll just fall back to the system default.
            catch (CultureNotFoundException)
            {
                return;
            }
            catch (ArgumentException)
            {
                return;
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
