using Egitim.API.Abstract;
using Egitim.API.Concrate;
using Egitim.API.DBEgitim;
using Egitim.API.DBEgitim.ConnectionStrings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Egitim.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string key = "Karabiberim vur kadehlere hadi içelim, içelim her gece zevki sefa, doldu gönmlüme. Hadi içelim, acıların yerine...";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            EgitimSQLServer.Title = "Eğitim veritabanı MSSQL";
            EgitimSQLServer.Text = Configuration.GetConnectionString("EgitimSQLServer");

            EgitimOracle.Title = "Eğitim veritabanı Oracle";
            EgitimOracle.Text = Configuration.GetConnectionString("EgitimOracle");

            //AddDbContext metodu yazılması gerek.
            services.AddDbContext<Context>(options => options.UseSqlServer(EgitimSQLServer.Text, b => b.MigrationsAssembly("Egitim.API")));

            services.AddScoped<IToken, Token>();
            services.AddScoped<IYoneticiRepository, YoneticiRepository>();

            services.AddControllers();

            services.AddDistributedRedisCache(options => {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });

            //Memory caching mekanizmasını kullanabilmek için ekledik
            services.AddMemoryCache();

            //Responce caching mekanizmasını kullanabilmek için ekledik ve mekanizmayı düzenledik.
            services.AddResponseCaching(_ =>
            {
                _.MaximumBodySize = 64; //Response Body’ler için geçerli maksimum boyut. Varsayılan olarak 64 MB’tır.
                _.SizeLimit = 100; //Response Cache’in maksimum ne kadar boyutta tutulacağını belirtiriz. Varsayılan olarak 100 MB değerine sahiptir.
                _.UseCaseSensitivePaths = false; //Path değerinin büyük ya da küçük harf duyarlığında olup olmamasını belirler.
            });

            // JWT authentication Aayarlaması
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; //metadata gerekmesin
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //Her response'un header'ına verinin oluşturulma tarihini ekleyebilmek için yazdık.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Data-Created-Date", DateTime.Now.ToString());

                await next();
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //Token bazlı doğrulamayı kullanabilmek için ekledik.

            app.UseAuthorization();

            app.UseResponseCaching(); //Responce caching mekanizmasını kullanabilmek için ekledik.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
