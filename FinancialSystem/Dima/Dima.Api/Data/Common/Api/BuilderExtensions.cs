using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data.Common.Api
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder webApplicationBuilder)
        {
            Configuration.ConnectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = webApplicationBuilder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = webApplicationBuilder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName)); // gera o front para consultar a documentacao da api
        }

        public static void AddSecurity(this WebApplicationBuilder webApplicationBuilder) 
        {
            webApplicationBuilder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
            webApplicationBuilder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.ConnectionString);
            });

            webApplicationBuilder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();
        }

        public static void AddServices(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            webApplicationBuilder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddCors(options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName, policy => policy
                .WithOrigins([
                    Configuration.BackendUrl,
                    Configuration.FrontendUrl
                ])
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                ));
        }
    }
}
