using PizzaOrder.Services;

<<<<<<< Updated upstream
using Scalar.AspNetCore;
=======
>>>>>>> Stashed changes

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IOrderStateService, OrderStateService>();
builder.Services.AddControllers().AddDapr();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();