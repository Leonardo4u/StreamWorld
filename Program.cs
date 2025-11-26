using Microsoft.EntityFrameworkCore;
using StreamWorld.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core with SQLite (simple local file DB)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=catalog.db"));

// Cookie authentication for admin area (simple, no Identity)
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.Cookie.Name = "StreamWorld.Auth";
    });

// Authorization policy for admin users (checks claim Role=Admin)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Role", "Admin"));
});

var app = builder.Build();

// Ensure database is created and tables exist. For development we'll create missing tables if needed.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    // Seed inicial (pt-BR) se estiver vazio
    if (!db.Generos.Any())
    {
        var gAcao = new StreamWorld.Models.Genero { Nome = "Ação" };
        var gDrama = new StreamWorld.Models.Genero { Nome = "Drama" };
        var gComedia = new StreamWorld.Models.Genero { Nome = "Comédia" };
        var gSciFi = new StreamWorld.Models.Genero { Nome = "Ficção Científica" };
        db.Generos.AddRange(gAcao, gDrama, gComedia, gSciFi);

        var a1 = new StreamWorld.Models.Artista { Nome = "John Doe" };
        var a2 = new StreamWorld.Models.Artista { Nome = "Jane Smith" };
        var a3 = new StreamWorld.Models.Artista { Nome = "Alice Johnson" };
        db.Artistas.AddRange(a1, a2, a3);

        db.SaveChanges();

        var p1 = new StreamWorld.Models.Producao { Titulo = "Edge of Tomorrow", Tipo = "Filme", GeneroId = gSciFi.Id, Sinopse = "Filme de ação e ficção científica.", DataLancamento = new DateTime(2014,6,6) };
        var p2 = new StreamWorld.Models.Producao { Titulo = "The Office Reunion", Tipo = "Série", GeneroId = gComedia.Id, Sinopse = "Série de comédia.", DataLancamento = new DateTime(2010,1,1) };
        var p3 = new StreamWorld.Models.Producao { Titulo = "Deep Feelings", Tipo = "Filme", GeneroId = gDrama.Id, Sinopse = "Uma história dramática.", DataLancamento = new DateTime(2018,3,12) };
        db.Producoes.AddRange(p1, p2, p3);
        db.SaveChanges();

        db.ProducaoArtistas.Add(new StreamWorld.Models.ProducaoArtista { ProducaoId = p1.Id, ArtistaId = a1.Id });
        db.ProducaoArtistas.Add(new StreamWorld.Models.ProducaoArtista { ProducaoId = p1.Id, ArtistaId = a2.Id });
        db.ProducaoArtistas.Add(new StreamWorld.Models.ProducaoArtista { ProducaoId = p2.Id, ArtistaId = a3.Id });
        db.ProducaoArtistas.Add(new StreamWorld.Models.ProducaoArtista { ProducaoId = p3.Id, ArtistaId = a2.Id });

        db.MensagensContato.Add(new StreamWorld.Models.MensagemContato { Nome = "Teste Usuario", Email = "teste@example.com", Mensagem = "Mensagem de teste" });

        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
