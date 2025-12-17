using BFF.Web.DTOs;
using BFF.Web.Filters;
using BFF.Web.Interfaces.Integrations;
using BFF.Web.Interfaces.Repositories;
using BFF.Web.Interfaces.Services;
using BFF.Web.Middlewares;
using BFF.Web.Repositories;
using BFF.Web.Services;
using BFF.Web.Transformations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQueryBuilder<EpisodiosFilter>, EpisodeQueryBuilder>();
builder.Services.AddScoped<ITranformations<EpisodioDto>, EpisodeTransformations>();
builder.Services.AddTransient<IEpisodiosRepository, EpisodiosAPIRepository>();
builder.Services.AddHttpClient<IEpisodiosRepository, EpisodiosAPIRepository>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["core:rickandmortyBaseURL"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
builder.Services.AddTransient<IItemInfoService<EpisodioDto>, EpisodiosInfoService>();

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
