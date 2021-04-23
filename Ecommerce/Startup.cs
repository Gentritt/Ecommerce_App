 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Ecommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce
{
	public class Startup
	{
		private readonly IConfiguration _config;
		private readonly IWebHostEnvironment _env;

		public Startup(IConfiguration configuration,IWebHostEnvironment env)
		{
			_config = configuration;
			_env = env;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddIdentity<UsersStore, IdentityRole>(cfg =>
			{
				cfg.User.RequireUniqueEmail = true;
			})
				.AddEntityFrameworkStores<EcommerceContext>();
			//services.AddDbContext<EcommerceContext>(cfg =>
			//cfg.UseSqlServer(Configuration.GetConnectionString("EcommerceConnectionString"))
			//);

			services.AddAuthentication()
				.AddCookie()
				.AddJwtBearer(
				cfg=>
				{
					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuer = _config["Tokens:Issuer"],
						ValidAudience = _config["Tokens:Audience"], 
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
					};
				});
				
			services.AddDbContextPool<EcommerceContext>(options => options.UseSqlServer(_config.GetConnectionString("EcommerceDbConnection")));


			
			services.AddTransient<EcommerceSeeder>(); // Calling this seeder to add the data to database
			services.AddTransient<IMailService, NullMailService>(); // Transient contains methods that do things
																	//The implementation of NullMailService by Interface.
																	//support for mail service

			services.AddScoped<IEcommerceRepository, EcommerceRepository>();

			var mappperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});

			IMapper mapper = mappperConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddControllersWithViews(opt=> {
				if (_env.IsProduction())
				{
					opt.Filters.Add(new RequireHttpsAttribute());
				}

			})	.AddNewtonsoftJson(opt=> opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

		

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseAuthentication();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();



			//app.UseDefaultFiles();
			//app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=App}/{action=Index}/{id?}");
			});

			if (env.IsDevelopment())
			{
				//seed the database
				 using(var scope = app.ApplicationServices.CreateScope())
				{
					var seeder = scope.ServiceProvider.GetService<EcommerceSeeder>();
					seeder.Seed().Wait();
				}
			}
		}
	}
}
