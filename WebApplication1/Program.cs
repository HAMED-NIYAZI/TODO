using Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using IOC.DependencyContainer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
 
builder.Services.AddSwaggerGen(c =>
{
     c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


#region AddDbContext
builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TODODBConnection"));
});
#endregion

#region Add Services

builder.Services.RegisterServices();
#endregion

#region Cors Policy
builder.Services.AddCors(p => p.AddPolicy("MyCorsPolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

#endregion

#region Add jwt

var secretKey = builder.Configuration.GetValue<string>("TokenKey");
var tokenTimeOut = builder.Configuration.GetValue<int>("TokenTimeOut");

var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        //برای کنترل زمان توکن
        ClockSkew = TimeSpan.FromMinutes(tokenTimeOut),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("MyCorsPolicy");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
