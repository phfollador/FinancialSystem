var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Request
// GET, POST, PUT & DELETE
// GET (nao tem corpo)
// REQUEST -> cabecalho e corpo

app.MapGet("/", () => "Hello World!");

app.Run();
