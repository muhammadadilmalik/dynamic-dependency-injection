﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApplication.Core.BootStrap;

namespace TestApplication
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
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			var type = typeof(ITransientDependency);
			foreach (var classType in GetTypes(type))
			{
				var interfaceType = Type.GetType(GetInterfaceFullName(classType.FullName));
				services.AddTransient(interfaceType, classType);
			}

			var singletype = typeof(ISingletonDependency);
			foreach (var classType in GetTypes(singletype))
			{
				var interfaceType = Type.GetType(GetInterfaceFullName(classType.FullName));
				services.AddSingleton(interfaceType, classType);
			}

			var scopetype = typeof(IScopedDependency);
			foreach (var classType in GetTypes(scopetype))
			{
				var interfaceType = Type.GetType(GetInterfaceFullName(classType.FullName));
				services.AddScoped(interfaceType, classType);
			}
		}
		private IEnumerable<Type> GetTypes(Type type)
		{
			return System.Reflection
							.Assembly
							.GetExecutingAssembly()
							.GetTypes()
							.Where(x => x.GetInterfaces().Contains(type));
		}
		private string GetInterfaceFullName(string classFullName)
		{
			var tempInterface = classFullName.Split(".");
			tempInterface[tempInterface.Length - 1] = String.Concat("I", tempInterface[tempInterface.Length - 1]);
			return String.Join(".", tempInterface);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}