
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Reflection;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
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
                // temporarily until launchSettings is solved
                // currently it is fetched from appsettings
                // the connection string alias needs to be changed according to dev
                var conString = builder.Configuration.GetConnectionString("TicketMasterDatabase") ??
                                throw new InvalidOperationException("Connection string 'TicketMasterDatabase' not found.");
                options.UseSqlServer(conString);
                //options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:UserDatabase"));
                //launchsettings.json -> environmentVariables -> ConnectionStrings:UserDatabase
            });
            //Server=localhost;Database=TicketMaster;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true
            //PM> Add-Migration Init -Project TicketMaster.DataContext
            //PM> Update-Database //network error -> rossz connection-string

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<UnitOfWork>();

            // Adding Services to the scope
            builder.Services.AddScoped<IFilmService, FilmService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketMaster" });
            });

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
