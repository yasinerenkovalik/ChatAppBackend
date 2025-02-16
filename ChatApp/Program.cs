using System.Text;
using AutoMapper;
using ChatApp;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>{
     c.SwaggerDoc("v1",new OpenApiInfo{Title="Chat app web apı", Version="v1"});
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme{
        Description="jwt authorization header using the Bearer scheme",
        Name="Authorization",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme="Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
        new OpenApiSecurityScheme{
            Reference=new OpenApiReference{
                Type=ReferenceType.SecurityScheme,
            Id="Bearer"
            }
        },
        Array.Empty<string>()
        }
    });
    
    });

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatAppDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.AddSingleton<ISignalrConnection,SignalrConnection>();

//cors

var corsPolicyName="CorsPolcy";
builder.Services.AddCors(options=>{
    options.AddPolicy(corsPolicyName,builder=>{
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//AUTO MAPPER
var mapperConfig=new MapperConfiguration(mc=>{
    mc.AddProfile(new MyProfiles());
});
IMapper mapper=mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>{
    options.TokenValidationParameters=new TokenValidationParameters{
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=builder.Configuration["Jwt:Issure"],
        ValidAudience=builder.Configuration["Jwt:Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))

    };
});


var app = builder.Build();

app.UseCors(corsPolicyName);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

//app.UseHttpsRedirection();
//app.MapDefaultControllerRoute();
app.UseEndpoints(endpoints=>{
    endpoints.MapControllers();
    endpoints.MapHub<ChatHub>("/chathub");

});

app.UseAuthorization();
app.Run();