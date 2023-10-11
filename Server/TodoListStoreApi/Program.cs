using TodoListStoreApi.Models;
using TodoListStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000") // Reemplaza con la URL de tu aplicación de React
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.
builder.Services.Configure<TodoListStoreDatabaseSettings>(
    builder.Configuration.GetSection("TodoListDatabase"));

builder.Services.AddSingleton<TodoListService>();

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

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
