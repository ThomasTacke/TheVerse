using BaiHu.Services;
using dotenv.net;
using MQTTnet;
using MQTTnet.Client;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<MqttClientService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// MQTT Client Connect & Setup
var mqttClient = app.Services.GetService<MqttClientService>();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
app.UseSwagger(c => c.RouteTemplate = "bai-hu/api/{documentName}/swagger/swagger.json");
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("swagger/swagger.json", "BaiHu v1");
    c.RoutePrefix = "bai-hu/api/v1";
});
// }

app.UseAuthorization();

app.MapControllers();

app.Run();
