using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName)); // gera o front para consultar a documentacao da api
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Request
// GET, POST, PUT & DELETE
// GET (nao tem corpo)
// REQUEST -> cabecalho e corpo

// endpoints - url para acesso (https://localhos:0000/)

// versionamento: serao diversos fronts consumindo a api
// mas pode haver alguma modificacao na api e determinada rota pode quebrar

app.MapPost(
    "/v1/categories", 
    (CreateCategoryRequest request, Handler handler) => handler.handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.Run();