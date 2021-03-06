using EasyMemoryCache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using MatrixAuth.Service.Class;
using MatrixAuth.Service.Interface;

namespace MatrixAuth.API
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
            //services.AddAuthentication(authOptions =>
            //{
            //    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(bearerOptions =>
            //{
            //    var paramsValidation = bearerOptions.TokenValidationParameters;
            //    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
            //    paramsValidation.ValidAudience = tokenConfigurations.Audience;
            //    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

            //    paramsValidation.ValidateIssuerSigningKey = true;

            //    paramsValidation.ValidateLifetime = true;

            //    paramsValidation.ClockSkew = TimeSpan.Zero;
            //});

            //services.AddAuthorization(auth =>
            //{
            //    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            //        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser().Build());
            //});

            services.AddMvcCore()
                .AddApiExplorer();

            //services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            //{
            //    builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //}));

            services.AddSingleton<IMatrixAuthService, MatrixAuthService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ICaching, Caching>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Member",
            //        policy => policy.RequireClaim("MembershipId"));
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MatrixAuth API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            //app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MatrixAuthAPIClient v1");
            });
        }
    }
}