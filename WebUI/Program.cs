using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using WebUI.Models;

namespace WebUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			
			builder.Services.AddSingleton<LanguageService>();
			builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
			builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
			{
				options.DataAnnotationLocalizerProvider = (type, factory) =>
				{
					var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
					return factory.Create(nameof(SharedResource), assemblyName.Name);
				};
			});
			builder.Services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo> {
					new CultureInfo("tr-TR"),
					new CultureInfo("en-US")
	                };
				options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
				options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
			});

			// Add services to the container.
			builder.Services.AddControllersWithViews();


			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
			app.UseRouting();
			app.UseAuthorization();
			app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
			app.Run();
		}
	}
}