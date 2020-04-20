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

            // DB Migration iþlemlerinde migration dosyalarýnýn Data katmanýnda saklanabilmesi için sqlite options'a þu tanýmlamayý yapýyoruz.  y => y.MigrationsAssembly("Data")
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("MySql"), y => y.MigrationsAssembly("Data")));
            services.AddScoped(typeof(Data.Infrastructure.IUnitOfWork), typeof(Data.Infrastructure.UnitOfWork));

            // Repository'leri tek tek burada tanýmlamak istemediðimizde bu þekilde tanýmlayabiliriz.
            // Bu kullanýmýn sorunu, repository için modeller (T) çaðýrýldýðý yerden tanýmlanacaðý için tek noktadan deðiþiklik yapýlmasý proje içerisinde kod deðiikliðne ihtiyaç duymasýdýr.
            // Bu sorunu yaþamamak için her bir repository eþleþmesini ayrý ayrý olarak services'e eklemek daha doðru olacaktýr.
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
