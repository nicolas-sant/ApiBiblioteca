var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var livros = new List<LivroDto>
{
    new LivroDto(1, "A Mortre de Ivan Ilitch", "Lev Tolstói", 1886),
    new LivroDto(2, "O Estrangeiro", "Albert Camus", 1942),
    new LivroDto(1, "A Morte Feliz", "Albert Camus", 1971),
};

app.MapGet("/api/livros", () =>
{
    return Results.Ok(livros);
});

app.MapPost("/api/livros", (LivroEntradaDto dados) =>
{
    int proximoId = livros.Count + 1;
    var novoLivro = new LivroDto(proximoId, dados.Titulo, dados.Autor, dados.DataDePublicacao);

    livros.Add(novoLivro);

    return Results.Created($"/api/livros/{novoLivro.Id}", novoLivro);
});

app.Run();

record LivroDto(int Id, string Titulo, string Autor, int DataDePublicacao);
record LivroEntradaDto(string Titulo, string Autor, int DataDePublicacao);
