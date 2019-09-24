using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiSoccerTeams.Models;

namespace WebApiSoccerTeams
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("soccerTeamDB"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(ConfigureJson);
        }

        private void ConfigureJson(MvcJsonOptions obj)
        {
            obj.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            if (!context.Teams.Any())
            {
                context.Teams.AddRange(new List<Team>()
                {
                    new Team(){ Name = "Milan", Players = 
                                                    new List<Player>(){
                                                        new Player(){ Name = "Luis", LastName = "Alarez", Number = 10},
                                                        new Player(){ Name = "Daniel", LastName = "Alarez", Number = 17},
                                                        new Player(){ Name = "Roberto", LastName = "Velasco", Number = 13}
                                                    }
                    },
                    new Team(){ Name = "Valle del Sol", Players =
                                                            new List<Player>(){
                                                                new Player(){ Name = "Gabriel", LastName = "yyyy", Number = 11},
                                                                new Player(){ Name = "Chopo", LastName = "zzzz", Number = 27},
                                                                new Player(){ Name = "Test", LastName = "test", Number = 43}
                                                            }
                    },
                     new Team(){ Name = "Panaderos", Players =
                                                        new List<Player>(){
                                                            new Player(){ Name = "Chanchi", LastName = "Hernandez", Number = 19},
                                                            new Player(){ Name = "Marcos", LastName = "Valdez", Number = 11},
                                                            new Player(){ Name = "Toro", LastName = "xxx", Number = 7}
                                                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
