using BFF.Web.DTOs;
using BFF.Web.Interfaces;
using BFF.Web.Middlewares;
using BFF.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IItemInfoService<EpisodioDto>, EpisodiosInfoService>();
builder.Services.AddHttpClient<IItemInfoService<EpisodioDto>, EpisodiosInfoService>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["core:rickandmortyBaseURL"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddControllers();

// Agregar el servicio CORS
var politicaPermisosCors = "_politicaPermisosCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: politicaPermisosCors,
        policy =>
        {
            policy
                .AllowAnyOrigin() 
                .AllowAnyHeader() 
                .AllowAnyMethod(); 
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(politicaPermisosCors);
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapGet("/testErrorHandler", () =>
{
    throw new Exception("exception de prueba");
})
.WithName("errorHandler")
.WithOpenApi();

app.MapControllers();

app.Run();
