using Api;
using Api.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiTests.Functional.Helpers
{
    public class TestFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .ConfigureServices(services =>
                {
                    services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName: "Kennisdeling"));
                })

                .UseEnvironment("Development")
                .UseStartup<Startup>();
        }
    }
}