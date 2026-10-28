using FaceDetection.service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<FaceDetectionService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();
