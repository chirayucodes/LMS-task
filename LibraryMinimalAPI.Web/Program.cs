using LibraryMinimalAPI.Persistence;
using LibraryMinimalAPI.Services;
using LibraryMinimalAPI.Web.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services
    .AddScoped<BookService>()
    .AddScoped<CategoryService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

RouteGroupBuilder bookGroup = app.MapGroup("api");

bookGroup.MapBookEndpoints()
    .MapCategoryEndpoints();

app.MapGet("/", () => "Welcome to the Library API!");

app.Run();