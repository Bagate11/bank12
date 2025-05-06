
using BankBlazor.API.Context;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var builder = WebApplication.CreateBuilder(args);




            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connString = builder.Configuration.GetConnectionString("BankBlazor");
            builder.Services.AddDbContext<BankBlazorContext>(options =>
            options.UseSqlServer(connString));



            var app = builder.Build(); 

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
