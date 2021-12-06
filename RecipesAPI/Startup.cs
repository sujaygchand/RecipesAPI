using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.IO;

namespace RecipesAPI
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
			services.AddControllers();

			services.AddSwaggerGen(options => {
				options.SwaggerDoc("RecipesOpenAPISpec", new Microsoft.OpenApi.Models.OpenApiInfo 
				{ 
					Title = "Recipes API",
					Description = "Example API For Recipes",
					Version = "v1"
				});

				var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, filename);
				options.IncludeXmlComments(xmlCommentsFullPath);
			});
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

			app.UseSwagger();

			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/RecipesOpenAPISpec/swagger.json", "Recipes API");
				options.RoutePrefix = "";
			});
		}
	}
}
