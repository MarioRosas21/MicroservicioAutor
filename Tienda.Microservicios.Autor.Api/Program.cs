using Tienda.Microservicios.Autor.Api.extensions;
using Tienda.Microservicios.Autor.Api.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomServices(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3001",
            "https://libreria-ashen.vercel.app"
            )
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Verificación de conexión
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ContextoAutor>();
    try
    {
        if (db.Database.CanConnect())
            Console.WriteLine("Conexión a la base de datos exitosa.");
        else
            Console.WriteLine("No se pudo conectar a la base de datos.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
    }
}

// Pipeline
//if (app.Environment.IsDevelopment())
//{
    
//}

app.UseSwagger();
app.UseSwaggerUI();
app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); // <- AQUÍ VA CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
