using Dima.Core;

namespace Dima.Api.Data.Common.Api
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder webApplicationBuilder)
        {
            Configuration.ConnectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }
    }
}
