using System.Security.Claims;
using System.Text;
using LostTimeWeb.DAL;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LostTimeWeb.WebApp
{
    public class Startup
    {
        public Startup( IHostingEnvironment env )
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath( env.ContentRootPath )
                .AddJsonFile( "appsettings.json", optional: true, reloadOnChange: true )
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices( IServiceCollection services )
        {
            services.AddOptions();

            string secretKey = Configuration[ "JwtBearer:SigningKey" ];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( secretKey ) );

            services.Configure<TokenProviderOptions>( o =>
            {
                o.Audience = Configuration[ "JwtBearer:Audience" ];
                o.Issuer = Configuration[ "JwtBearer:Issuer" ];
                o.SigningCredentials = new SigningCredentials( signingKey, SecurityAlgorithms.HmacSha256 );
            } );

            services.AddMvc();
            services.AddSingleton( _ => new UserGateway( Configuration[ "ConnectionStrings:PrimarySchoolDB" ] ) );
            services.AddSingleton( _ => new ClassGateway( Configuration[ "ConnectionStrings:PrimarySchoolDB" ] ) );
            services.AddSingleton( _ => new StudentGateway( Configuration[ "ConnectionStrings:PrimarySchoolDB" ] ) );
            services.AddSingleton( _ => new TeacherGateway( Configuration[ "ConnectionStrings:PrimarySchoolDB" ] ) );
            services.AddSingleton<PasswordHasher>();
            services.AddSingleton<UserService>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<ClassService>();
            services.AddSingleton<StudentService>();
            services.AddSingleton<TeacherService>();
            services.AddSingleton<GitHubService>();
            services.AddSingleton<GitHubClient>();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            loggerFactory.AddConsole();

            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            string secretKey = Configuration[ "JwtBearer:SigningKey" ];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes( secretKey ) );

            app.UseJwtBearerAuthentication( new JwtBearerOptions
            {
                AuthenticationScheme = JwtBearerAuthentication.AuthenticationScheme,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    ValidateIssuer = true,
                    ValidIssuer = Configuration[ "JwtBearer:Issuer" ],

                    ValidateAudience = true,
                    ValidAudience = Configuration[ "JwtBearer:Audience" ],

                    NameClaimType = ClaimTypes.Email,
                    AuthenticationType = JwtBearerAuthentication.AuthenticationType
                }
            } );

            app.UseCookieAuthentication( new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthentication.AuthenticationScheme
            } );

            ExternalAuthenticationEvents githubAuthenticationEvents = new ExternalAuthenticationEvents(
                new GithubExternalAuthenticationManager( app.ApplicationServices.GetRequiredService<UserService>() ) );
            ExternalAuthenticationEvents googleAuthenticationEvents = new ExternalAuthenticationEvents(
                new GoogleExternalAuthenticationManager( app.ApplicationServices.GetRequiredService<UserService>() ) );

            app.UseGitHubAuthentication( o =>
            {
                o.SignInScheme = CookieAuthentication.AuthenticationScheme;
                o.ClientId = Configuration[ "Authentication:Github:ClientId" ];
                o.ClientSecret = Configuration[ "Authentication:Github:ClientSecret" ];
                o.Scope.Add( "user" );
                o.Scope.Add( "user:email" );
                o.Events = new OAuthEvents
                {
                    OnCreatingTicket = githubAuthenticationEvents.OnCreatingTicket
                };
            } );

            app.UseGoogleAuthentication( c =>
            {
                c.SignInScheme = CookieAuthentication.AuthenticationScheme;
                c.ClientId = Configuration[ "Authentication:Google:ClientId" ];
                c.ClientSecret = Configuration[ "Authentication:Google:ClientSecret" ];
                c.Events = new OAuthEvents
                {
                    OnCreatingTicket = googleAuthenticationEvents.OnCreatingTicket
                };
                c.AccessType = "offline";
            } );

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(webSocketOptions);   
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" } );

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "Home/{*anything}",
                    defaults: new { controller = "Home", action = "Index" } );
            } );
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                    {
                        if (context.WebSockets.IsWebSocketRequest)
                        {
                            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            await Echo(context, webSocket);
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                        }
                    }
                else
                {
                    await next();
                }
            });

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

            app.UseStaticFiles();
        }
    }
}
