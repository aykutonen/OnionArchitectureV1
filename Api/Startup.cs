using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Api
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
            services.AddControllers().AddNewtonsoftJson();

            // DB Migration i�lemlerinde migration dosyalar�n�n Data katman�nda saklanabilmesi i�in sqlite options'a �u tan�mlamay� yap�yoruz.  y => y.MigrationsAssembly("Data")
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), y => y.MigrationsAssembly("Data")));
            services.AddScoped(typeof(Data.Infrastructure.IUnitOfWork), typeof(Data.Infrastructure.UnitOfWork));

            // Repository'leri tek tek burada tan�mlamak istemedi�imizde bu �ekilde tan�mlayabiliriz.
            // Bu kullan�m�n sorunu, repository i�in modeller (T) �a��r�ld��� yerden tan�mlanaca�� i�in tek noktadan de�i�iklik yap�lmas� proje i�erisinde kod de�iikli�ne ihtiya� duymas�d�r.
            // Bu sorunu ya�amamak i�in her bir repository e�le�mesini ayr� ayr� olarak services'e eklemek daha do�ru olacakt�r.
            services.AddScoped(typeof(Data.Infrastructure.IRepository<>), typeof(Data.Infrastructure.RepositoryBase<>));

            services.AddTransient(typeof(Service.IToDoService), typeof(Service.ToDoService));
            services.AddTransient(typeof(Service.IUserService), typeof(Service.UserService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
