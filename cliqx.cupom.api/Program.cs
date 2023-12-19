using cliqx.cupom.api;
using cliqx.cupom.api.Dependencies;
using cliqx.cupom.api.Repositories;
using cliqx.cupom.api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api de cupom snog", Version = "v1" });
    });

    var connString = builder.Configuration.GetConnectionString("MariaDBContext");

    builder.Services.AddDbContext<DataContext>(
                x => x.UseMySql(connString, ServerVersion.AutoDetect(connString))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());

    builder.Services.AddMyDependencyGroup();

    var app = builder.Build();

    app.UseSwagger();
    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API cupom snog v1"));
    app.UseCors("CorsPolicy");
    app.Run();

    Console.WriteLine("Aplicação iniciada");

}
catch (Exception ex)
{
    Console.WriteLine("Erro ao iniciar a aplicação: " + ex.ToString());
}
