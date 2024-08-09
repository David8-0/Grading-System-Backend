
using Grading_System_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Grading_System_Backend.UnitOfWorks;
using Grading_System_Backend.Services;

namespace Grading_System_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string allowCors = "";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<Context>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("main")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(allowCors, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<SubjectService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(allowCors);
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
