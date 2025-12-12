using Dima.Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.CustomSchemaIds(n => n.FullName)); // gera o front para consultar a documentacao da api
builder.Services.AddTransient<Handler>();

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
    "/v1/transactions", 
    (Request request, Handler handler) => handler.handle(request))
    .WithName("Transactions: Create")
    .WithSummary("Cria uma nova transaction")
    .Produces<Response>();

app.Run();

// Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

// Response
public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
}

// Handler
public class Handler()
{
    public Response handle(Request request)
    {
        return new Response { Id = 4, Title = request.Title};
    }
}