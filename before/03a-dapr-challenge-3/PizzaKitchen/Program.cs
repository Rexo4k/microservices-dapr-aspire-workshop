using PizzaKitchen.Services;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICookService, CookService>();
<<<<<<< Updated upstream
builder.Services.AddOpenApi();
=======
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddDapr();
>>>>>>> Stashed changes

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCloudEvents();
app.MapControllers();
app.Run();