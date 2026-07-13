using Microsoft.EntityFrameworkCore;
using Todolist2.Application.CommandHandlers;
using Todolist2.Application.QuarieHandlers;
using Todolist2.Application.Quaries;
using Todolist2.Infrastructure.Data;
using Todolist2.Infrastructure.Repositories;

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
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IToDoRepository, ToDoRepositories>();
builder.Services.AddScoped<CTodoCommandHandlers>();
builder.Services.AddScoped<UTodoCommandHandlers>();
builder.Services.AddScoped<DTodoCommandHandlers>();
builder.Services.AddScoped<GetAllTodoquarieHandlers>();
builder.Services.AddScoped<GetTodoByIdquarieHandlers>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();