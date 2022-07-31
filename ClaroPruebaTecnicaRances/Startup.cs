using ClaroPruebaTecnicaRances.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPruebaTecnicaRances
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookDbContext>(option => option.UseInMemoryDatabase(Configuration.GetConnectionString(name: "DbBook")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClaroPruebaTecnicaRances", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClaroPruebaTecnicaRances v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<BookDbContext>();
            SeedData(context);
        }

        

        public static void SeedData(BookDbContext context)
        {
            Book Things_Fall_Apart = new Book {

                Id = 1,
                Title = "Things Fall Apart",
                Description = " is the debut novel by Nigerian author Chinua Achebe, first published in 1958.",
                PageCpunt = 209,
                Excerpt = "Okonkwo was well known throughout the nine villages and even beyond. His fame rested on solid personal achievements.",
                PublishDate = DateTime.Parse("01/01/1958"),
                

            };

            Book Fairy_tales = new Book
            {
                Id = 2,
                Title = "Fairy tales",
                Description = "Classic fairy tales by Hans Christian Andersen, The Brothers Grimm, Charles Perrault, Aesop and others.",
                PageCpunt = 784,
                Excerpt = "What could you write that would make you happy? As if my imagination had been waiting for the question to be asked, I saw a vast deserted city — deserted but alive.",
                PublishDate = DateTime.Parse("07/29/1836")
                

            };

            Book The_Divine_Comedy = new Book()
            {
                Id = 3,
                Title = "The Divine Comedy",
                Description = "The narrative takes as its literal subject the state of the soul after death and presents an image of divine justice meted out as due punishment or reward",
                PageCpunt = 928,
                Excerpt = "Because the Good, the object of the will, Is gathered all in it, and outside of the Light 105 What there is perfect is flawed [outside of the Light]. Now shall my telling of what I remember Fall far below the babbling of a baby Still bathing its tongue at the mother's breast.",
                PublishDate = DateTime.Parse("10/17/1315")
                

            };

           context.Books.Add(Things_Fall_Apart);
            context.Books.Add(Fairy_tales);
            context.Books.Add(The_Divine_Comedy);
            context.SaveChanges();

            Author Chinua_Achebe = new Author
            {
                Id = 1,
                IdBook = 1,
                FirstName = "Chinua",
                LastName = "Achebe"

            };

            Author Hans_Christian_Andersen = new Author
            {
                Id = 2,
                IdBook = 2,
                FirstName = "Hans Christian",
                LastName = "Andersen"
            };

            Author Dante_Alighieri = new Author
            {
                Id = 3,
                IdBook = 3,
                FirstName = "Dante",
                LastName = " Alighieri"
            };
            context.Authors.Add(Chinua_Achebe);
            context.Authors.Add(Hans_Christian_Andersen);
            context.Authors.Add(Dante_Alighieri);
            context.SaveChanges();

            CoverPhoto Things_FallApart = new CoverPhoto
            {
                Id = 1,
                IdBook = 1,
                Url = "https://localhost:44367/wwwroot/images/things-fall-apart.jpg"

            };

            CoverPhoto Fairytales = new CoverPhoto
            {
                Id = 2,
                IdBook = 2,
                Url = "https://localhost:44367/wwwroot/images/fairy-tales.jpg"

            };

            CoverPhoto The_DivineComedy = new CoverPhoto
            {
                Id = 3,
                IdBook = 3,
                Url = "https://localhost:44367/wwwroot/images/the-divine-comedy.jpg"

            };
            context.CoverPhotos.Add(Things_FallApart);
            context.CoverPhotos.Add(Fairytales);
            context.CoverPhotos.Add(The_DivineComedy);
            context.SaveChanges();

        }
    }
}
