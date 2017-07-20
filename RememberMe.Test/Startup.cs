using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RememberMe.Persistence;
using RememberMe.Core.Interfaces;
using RememberMe.Core.Models;
using RememberMe.Controllers;



namespace RememberMe.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IFriendRepository, FriendRepository>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            //services.AddAutoMapper(); 
            // services.AddDbContext<RememberMeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            // var options = new DbContextOptionsBuilder<RememberMeDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options; 
            //var context = new RememberMeDbContext(options); 
            services.AddDbContext<RememberMeDbContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));    

            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // var repository = app.ApplicationServices.GetService<IBrainstormSessionRepository>();
                // InitializeDatabaseAsync(repository).Wait();
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }

        // public async Task InitializeDatabaseAsync(IBrainstormSessionRepository repo)
        // {
        //     var sessionList = await repo.ListAsync();
        //     if (!sessionList.Any())
        //     {
        //         await repo.AddAsync(GetTestSession());
        //     }
        // }

        // public static BrainstormSession GetTestSession()
        // {
        //     var session = new BrainstormSession()
        //     {
        //         Name = "Test Session 1",
        //         DateCreated = new DateTime(2016, 8, 1)
        //     };
        //     var idea = new Idea()
        //     {
        //         DateCreated = new DateTime(2016, 8, 1),
        //         Description = "Totally awesome idea",
        //         Name = "Awesome idea"
        //     };
        //     session.AddIdea(idea);
        //     return session;
        // }
    }
}