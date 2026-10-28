using FaceEmbedding.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddSingleton<EmbeddingService>();

var app = builder.Build();

app.Services.GetRequiredService<EmbeddingService>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();