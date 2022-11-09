namespace JwtExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IPasswordHasher<CoreUser>, PasswordHasher<CoreUser>>();

            // TODO add services
            // builder.Services.AddTransient()

            builder.Services.AddCors(p => p.AddPolicy("corsapp", b => 
            {
                b.WithOrigin(builder.Configuration.GetSection("AllowOrigin").Get<string[]>()).AllowAnyHeader().AllowAnyMethod();
            }));

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext?.User ?? new ClaimsPrincipal());
        }
    }
}