namespace SampleProject.Gateway.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(configurationBuilder =>
                    {
                        var isUseDockerOcelot = Environment.GetEnvironmentVariable("USEDOCKEROCELOT"); //for the debugging purposes
                        if (isUseDockerOcelot != null && isUseDockerOcelot == "true")
                        {
                            configurationBuilder.AddJsonFile("ocelot.docker.json", false, true);
                        }
                        else
                        {
                            configurationBuilder.AddJsonFile("ocelot.json", false, true);
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}