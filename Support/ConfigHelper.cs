using Microsoft.Extensions.Configuration;

namespace ApiAuthomation.Support
{
    public class ConfigHelper
    {
        public Dictionary<string, string> EnvironmentVariables = new Dictionary<string, string>();

        public IConfiguration Configuration { get; set; }


        public ConfigHelper() 
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Configuration.GetSection("apiConfigurations").Bind(EnvironmentVariables);

        }

    }
}
