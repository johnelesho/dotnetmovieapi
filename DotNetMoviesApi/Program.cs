using DotNetMoviesApi;
using DotNetMoviesApi.APIBehavior;
using DotNetMoviesApi.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins(builder.Configuration.GetValue<String>("frontend_url") ?? string.Empty);
        });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ParsedBadRequest));
    options.Filters.Add(typeof(MyExceptionFilter));
}).ConfigureApiBehaviorOptions(BadRequestsBehavior.Parse);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var dbVersion = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(dbVersion, ServerVersion.AutoDetect(dbVersion));
});

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
// app.UseSwaggerUI();
// app.UseSwaggerUI(c =>
// {
//     c.RoutePrefix = "v1";
//     var basePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
//     c.SwaggerEndpoint($"{basePath}/swagger/{c.RoutePrefix}/swagger.json", "Name");
// });

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    options.DocumentTitle = "Smart Procedures UI";
});


app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();