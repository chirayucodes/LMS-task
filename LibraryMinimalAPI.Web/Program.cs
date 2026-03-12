using LibraryMinimalAPI.Persistence;
using LibraryMinimalAPI.Services;
using LibraryMinimalAPI.Web.Endpoints;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddScoped<BookService>()
    .AddScoped<CategoryService>()
    .AddScoped<MemberService>()
    .AddScoped<BookIssuedService>();
builder.Services. AddCors();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowAnyOrigin();
});

RouteGroupBuilder bookGroup = app.MapGroup("api");

bookGroup.MapBookEndpoints()
    .MapCategoryEndpoints()
    .MapMemberEndpoints()
    .MapBookIssuedEndpoints();

app.MapGet("/", () => "Welcome to the Library API!");

app.UseRouting();
app.Run();
