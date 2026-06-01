
namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddScoped<WebAPI.Services.ProduktService>();
            builder.Services.AddScoped<WebAPI.Interfaces.IGetDataInterface>(x => x.GetRequiredService<WebAPI.Services.ProduktService>());
            builder.Services.AddScoped<WebAPI.Interfaces.IFormSubmit>(x => x.GetRequiredService<WebAPI.Services.ProduktService>());
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<WebAPI.Interfaces.IGetDataInterface, WebAPI.Services.ProduktService>();
            builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
              policy.WithOrigins("http://localhost:4115").AllowAnyMethod().AllowAnyHeader()  ));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
