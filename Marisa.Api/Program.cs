using FluentValidation;
using Marisa.Api.Authentication;
using Marisa.Api.Controllers;
using Marisa.Api.ControllersInterface;
using Marisa.Domain.Authentication;
using Marisa.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("en");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IBaseController, BaseController>();

if (builder.Environment.IsDevelopment())
{ // Usar isso para os secrets, ir certo para cada "Pasta" "Shoope" Folder
    builder.Configuration.AddUserSecrets<Program>();

    //builder.Configuration.AddUserSecrets(typeof(DependectyInjection).Assembly);

    //var targetDirectories = new List<string>
    //{
    //    @"C:\Users\lucas\Desktop\marisa-clone-backend\Marisa.Api\bin\Debug\net8.0",
    //    @"C:\Users\lucas\Desktop\marisa-clone-backend\Marisa.Application\bin\Debug\net8.0",
    //    @"C:\Users\lucas\Desktop\marisa-clone-backend\Marisa.Domain\bin\Debug\net8.0",
    //    @"C:\Users\lucas\Desktop\marisa-clone-backend\Marisa.Infra.Data\bin\Debug\net8.0",
    //    @"C:\Users\lucas\Desktop\marisa-clone-backend\Marisa.Infra.IoC\bin\Debug\net8.0"
    //};

    //// Verificar se os assemblies estão carregados e carregar os secrets
    //foreach (var dir in targetDirectories)
    //{
    //    if (Directory.Exists(dir))
    //    {
    //        var dllFiles = Directory.GetFiles(dir, "*.dll");

    //        foreach (var dllFile in dllFiles)
    //        {
    //            var assembly = Assembly.LoadFrom(dllFile);
    //            builder.Configuration.AddUserSecrets(assembly);
    //        }
    //    }
    //}
}

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolity", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

//var keyJwtBearerSecret = builder.Configuration["Key:Jwt"];
var keyJwtBearerSecret = Environment.GetEnvironmentVariable("KEY_JWT");
if (keyJwtBearerSecret != null)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwtBearerSecret));

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddSignalR();

var app = builder.Build();

app.UseRouting();
app.UseCors("CorsPolity");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
