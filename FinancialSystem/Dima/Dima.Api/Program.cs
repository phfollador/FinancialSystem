var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Request
// GET, POST, PUT & DELETE
// GET (nao tem corpo)
// REQUEST -> cabecalho e corpo

// endpoints - url para acesso (https://localhos:0000/)
app.MapGet("/get", () => "Hello World!");
app.MapPost("/post", () => "Hello World!");
app.MapPut("/put", () => "Hello World!");
app.MapDelete("/delete", () => "Hello World!");

app.Run();
