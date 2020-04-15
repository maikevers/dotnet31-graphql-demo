using Api.Database;
using Api.GraphQL;
using Api.GraphQL.Types;
using Api.GraphQL.Types.Author;
using Api.GraphQL.Types.Topic;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types;
using LanguageExt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            GetCosmosDbOptions().Do(o =>
            {
                services.AddDbContext<Context>(options => options.UseCosmos(o.AccountEndpoint, o.AccountKey, o.DatabaseName));
            }).IfNone(() => throw new InvalidOperationException("Invalid Cosmos Db Options."));

            services.AddGraphQL(s => SchemaBuilder.New()
                .AddServices(s)
                .BindClrType<Guid, UuidType>()
                .AddType<UnitType>()
                .AddType<ErrorType>()
                .AddType<AuthorType>()
                .AddType<TopicType>()
                .AddQueryType<Queries>()
                .AddMutationType<Mutations>()
                .Create());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseGraphQL("/graphql");
            app.UsePlayground(new PlaygroundOptions { QueryPath = "/graphql", Path = "/playground" });
            EnsureDatabaseCreated(app);
        }

        private static void EnsureDatabaseCreated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<Context>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        private Option<CosmosDbOptions> GetCosmosDbOptions()
        {
            CosmosDbOptions cosmosDbOptions = new CosmosDbOptions();
            Configuration.GetSection("CosmosDb").Bind(cosmosDbOptions);
            cosmosDbOptions.AccountEndpoint = Configuration["CosmosDb:AccountEndpoint"];
            cosmosDbOptions.AccountKey = Configuration["CosmosDb:AccountKey"];

            if (cosmosDbOptions.AccountEndpoint != null &&
                cosmosDbOptions.AccountKey != null &&
                cosmosDbOptions.DatabaseName != null)
            {
                return cosmosDbOptions;
            }
            else
            {
                return Prelude.None;
            }
        }
    }
}