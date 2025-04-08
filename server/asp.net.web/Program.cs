var builder = WebApplication.CreateBuilder(args);

// Add services to the container before building the app.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Update to the React app's origin
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Allow credentials if necessary
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");  // Apply the CORS policy here
app.UseAuthorization();

app.MapControllers();

app.Run();