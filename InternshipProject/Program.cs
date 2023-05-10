using Microsoft.EntityFrameworkCore;

namespace InternshipProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql(connection));
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.MapControllers();

            app.Run();
        }
    }
}