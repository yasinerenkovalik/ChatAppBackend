using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatAppDbContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

//AUTO MAPPER
var mapperConfig=new MapperConfiguration(mc=>{
    mc.AddProfile(new MyProfiles());
});
IMapper mapper=mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapDefaultControllerRoute();


app.Run();

