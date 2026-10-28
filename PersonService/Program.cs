using Microsoft.EntityFrameworkCore;
using PersonService.Clients;
using PersonService.Data;
using PersonService.Repository;
using PersonService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PersonDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "PersonDatabase")));

builder.Services.AddHttpClient<FaceEmbeddingClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["Services:FaceEmbeddingUrl"]!);
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<PersonAppService>();


var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();