/*Modeule: EAD
Module Code: SE4040
Student Name: Jayawardena R.D.T.M
Student ID: IT20004354*/

using trms.api.Data;
using trms.api.Services;
using Microsoft.Extensions.DependencyInjection;
using trms.api.Common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins("http://example.com", "http://anotherdomain.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Register services
builder.Services.Configure<DbConfiguration>(builder.Configuration.GetSection("MongoDbConnection"));
builder.Services.AddScoped<MongoContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TrainService>();
builder.Services.AddScoped<BackOfficeService>();
builder.Services.AddScoped<ITravelAgentReservationService, TravelAgentReservationService>();
builder.Services.AddScoped<IMobileReservationServicesInterface, MobileReservationService>();


var app = builder.Build();
app.UseCors("AllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();