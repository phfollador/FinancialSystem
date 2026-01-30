using Dima.Core;

namespace Dima.Api.Data.Common.Api
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder webApplicationBuilder)
        {
            Configuration.ConnectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName)); // gera o front para consultar a documentacao da api
        }
    }
}
