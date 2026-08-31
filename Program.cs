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

app.MapPut("/api/livros/{id:int}", (int id LivroEntradaDto dados) =>
{
  int indice = livros.FindIndex(livroDaLista => livroDaLista.id == id);
  if (indice == -1)
  {
    return Results.NotFound();
  }

  var livroAtualizado = new LivroDto(id, dados.titulo, dados.autor, dados.dataDePublicacao);
  
  livros[indice] = livroAtualizado;

  return Results.Ok(livroAtualizado);
});

app.MapDelete("api/livros/{id:int}", (int id) =>
{
  int indice = livros.FindIndex(livroDaLista => livroDaLista.id == id);
  if (index == -1)
  {
    return Results.NotFound();
  }

  livros.RemoveAt(indice)
  
  return Results.NoContent();
});

app.Run();

record LivroDto(int Id, string Titulo, string Autor, int DataDePublicacao);
record LivroEntradaDto(string Titulo, string Autor, int DataDePublicacao);
