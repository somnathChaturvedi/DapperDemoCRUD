using DapperDemoCRUD.Context;
using DapperDemoCRUD.Contracts;
using DapperDemoCRUD.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DapperDemoCRUD
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<DapperContext>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddControllers();
			services.AddSwaggerGen();
			services.AddResponseCompression(options => {
				options.Providers.Add<GzipCompressionProvider>();
			});
			services.Configure<GzipCompressionProviderOptions>(options => { options.Level = System.IO.Compression.CompressionLevel.Fastest; });
		}

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
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
				//c.InjectStylesheet("/StyleSheet1.css");
			});
		}
	}
}
