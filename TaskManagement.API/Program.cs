using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManagement.API;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Mapper;
using TaskManagement.Infrastructure.Persistence;
using TaskManagement.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("TaskManagementCS");

builder.Services.AddDbContext<TaskDbContext>(options => options.UseInMemoryDatabase("DevEventsInMemory"));
//builder.Services.AddDbContext<TaskDbContext>(o => o.UseSqlServer(con));

builder.Services.AddAutoMapper(typeof(TaskManagementProfile).Assembly);
builder.Services.AddScoped<ITaskManagementService, TaskManagementService>();
builder.Services.AddScoped<ITaskManagementRepository, TaskManagementRepository>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TaskManagementService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
    });
}

//DatabaseManagementService.MigrationInit(app);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "TaskManagement.API",
//        Version = "v1",
//        Contact = new OpenApiContact
//        {
//            Name = "Eduardo",
//            Email = "e.ximenes17@hotmail.com",
//            Url = new Uri("https://www.linkedin.com/in/eduardo-l-ximenes/"),

//        }
//    });

//    var xmlFile = "TaskManagement.API.json";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    c.IncludeXmlComments(xmlPath);
//}); 

