
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.Services;

namespace TicketMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:UserDatabase"));
                //launchsettings.json -> environmentVariables -> ConnectionStrings:UserDatabase
            });
            //Server=localhost;Database=TicketMaster;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true
            //PM> Add-Migration Init -Project TicketMaster.DataContext
            //PM> Update-Database //network error -> rossz connection-string

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Adding FilmService to the scope
            builder.Services.AddScoped<IFilmService, FilmService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketMaster" });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
