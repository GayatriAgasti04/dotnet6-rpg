 global using dotnet6_rpg.Models;
using dotnet6_rpg.Data;
using dotnet6_rpg.Services.CharacterService;
using dotnet6_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
// using Microsoft.AspNetCore.AuthenticationJWtBearer;

var builder = WebApplication.CreateBuilder(args);
//var builder=WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>{
c.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
{
    Description="Standard Authorization header using the Bearer scheme ,e.g.\"bearer{token}\"",
    In=ParameterLocation.Header,
    Name="Authorization",
    Type=SecuritySchemeType.ApiKey
});

    c.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService,CharacterService>();
builder.Services.AddScoped<IAuthRepository,AuthRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(Options =>
{
    Options.TokenValidationParameters= new  TokenValidationParameters
    {
              ValidateIssuerSigningKey=true,
              IssuerSigningKey= new SymmetricSecurityKey(System.Text.Encoding.UTF8
              .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
              ValidateIssuer=false,
              ValidateAudience=false

    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IWeaponService,WeaponService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();


    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
