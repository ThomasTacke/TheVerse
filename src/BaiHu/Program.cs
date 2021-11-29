using dotenv.net;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
IMqttClient mqttClient = new MqttFactory().CreateMqttClient();
builder.Services.AddSingleton<IMqttClient>(mqttClient);
mqttClient.ConnectAsync(new MqttClientOptionsBuilder().WithClientId("BaiHu")
                                                      .WithTcpServer($"{Environment.GetEnvironmentVariable("MQTT_BROKER")}")
                                                      .WithCleanSession()
                                                      .Build());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
