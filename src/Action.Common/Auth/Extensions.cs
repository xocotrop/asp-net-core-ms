using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Action.Common.Auth
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            services.Configure<JwtOptions>(section);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication(auth => {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                var paramsValidation = cfg.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
                paramsValidation.ValidAudience = options.Audience;
                paramsValidation.ValidIssuer = options.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
                // Console.WriteLine(DateTime.Now);
                // Console.WriteLine(DateTime.UtcNow);
                // cfg.RequireHttpsMetadata = false;
                // cfg.SaveToken = true;
                // cfg.TokenValidationParameters = new TokenValidationParameters
                // {
                //     ValidateAudience = false,
                //     ValidIssuer = options.Issuer,
                //     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                //     ClockSkew = TimeSpan.Zero
                // };

            });
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            
        }
    }
}