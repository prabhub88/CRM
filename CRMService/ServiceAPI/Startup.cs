using Serilog;
using BusinessLayer.Utility;

namespace ServiceAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private static string configFileName = "appsettings_Dev.json";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext...
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

        }
        /*
        public static IHost InitializeBuild(string[] args)
        {

            InitializeConfigurations(args);
            return Host.CreateDefaultBuilder().ConfigureServices().UseSerilog().Build();
        }
        private static void InitializeConfigurations(string[] args)
        {
            try
            {
                Console.WriteLine("Init configurations....");
                var configBuilder = new ConfigurationBuilder();
                configBuilder.ConfigureSettingsFile(args);
                Configuration = configBuilder.Build();
                Console.WriteLine("Init logging");
               // InitializeLogging();
              //  Console.WriteLine("Fetching Key-vault info");
               // Configuration.GetHashicorpSecrets();
                Utilities.Config = Configuration;
            }
            catch (Exception ex) { Console.WriteLine("Excetion while initconfig on startup:" + ex.Message + ex.StackTrace); }
        }

        private static IHostBuilder ConfigureServices(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((services) =>
            {

                services.AddScoped<BusinessLayer.Utility.ILogger, FileLogger>();
            });
        }

        private static void ConfigureSettingsFile(this IConfigurationBuilder configBuilder, string[] args)
        {
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("envName")))
                Console.WriteLine("Please set the envName");
            else
                configFileName = "appsettings_" + Environment.GetEnvironmentVariable("envName") + ".json";

            Console.WriteLine("changing to " + configFileName + " File");

            configBuilder.AddJsonFile(configFileName, optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables().AddCommandLine(args);
        }
    }*/
    }
}
