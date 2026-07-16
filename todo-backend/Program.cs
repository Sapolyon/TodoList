using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoList.Api.Application.CommandHandlers;
using TodoList.Api.Application.QueryHandlers;
using TodoList.Api.Infrastructure.Data;
using TodoList.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo List API",
        Version = "v1",
        Description = "Todo görevlerini yönetmek için REST API."
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<CreateTodoCommandHandler>();
builder.Services.AddScoped<UpdateTodoCommandHandler>();
builder.Services.AddScoped<DeleteTodoCommandHandler>();
builder.Services.AddScoped<GetAllTodoQueryHandler>();
builder.Services.AddScoped<GetTodoByIdQueryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo List API v1");
        options.DocumentTitle = "Todo List API";
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();
