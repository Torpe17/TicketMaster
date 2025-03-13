
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
        //async lett a main, ha nem kell akkor "async Task" -> void
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var conString = builder.Configuration.GetConnectionString("Mark") ??
                            throw new InvalidOperationException("Connection string 'TicketMasterDatabase' not found.");
            builder.Services.AddDbContext<TicketMasterDbContext>(options =>
            {
               
                options.UseSqlServer(conString);
            });

            //Server=localhost;Database=TicketMaster;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true
            //PM> Add-Migration Init -Project TicketMaster.DataContext -Context TicketMasterDbContext //succeeded
            //PM> Update-Database //network error

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

            //crud test.... valaki oldja meg pls nekem mindig beraganak az adatok és csak clean solution->build solution után adja hozá a módosítottat... -Márk
            //A FilmService.cs ben vannak a crud műveletek, elvileg generikusak, teszt alapján elvileg mindenhez lehet velük hozzáadni... csak ugye ↑↑↑↑↑↑↑
            using (var scope = app.Services.CreateScope())
            {
                //dbcontext szedés
                var filmService = scope.ServiceProvider.GetRequiredService<IFilmService>();

                List<Film> existingFilms = await filmService.GetAllAsync<Film>();
                Console.WriteLine("Existing films in the database:");
                foreach (Film film in existingFilms)
                {
                    Console.WriteLine($"- {film.Title}");
                }

                // Random r = new Random();
                //string title = r.Next(1,1000).ToString();

                //ha nem fix értéket adok neki akkor vamaiért működik
                //mi
                //a
                //fasz
                Console.WriteLine("title:");
                string title =Console.ReadLine();
                
                //megölök valakit geci
                Film _film = new Film
                {
                    Title = title,
                    Description = "muk odj",
                    Director = "Drip Elek",
                    Genre = "vicc elek",
                    Length = 170
                };

                Console.WriteLine($"Attempting to add film: {_film.Title}");

                await filmService.AddAsync(_film);

                existingFilms = await filmService.GetAllAsync<Film>();
                Console.WriteLine("Updated films in the database:");
                foreach (var film in existingFilms)
                {
                    Console.WriteLine($"- {film.Title}");
                }
                //foreach (var film in existingFilms)
                //{
                //    await filmService.DeleteAsync(film);
                //}
            }

            app.Run();
            
        }
    }
}
