using ApiOAuthEmpleados.Data;
using ApiOAuthEmpleados.Helpers;
using ApiOAuthEmpleados.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());
// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryHospital>();
builder.Services.AddDbContext<HospitalContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Api seguridad empleados");
    options.RoutePrefix = "";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
