using dotenv.net;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace BaiHu.Services;

public class MqttListener {
    public static IMqttClientOptions MqttClientOptions = new MqttClientOptionsBuilder().WithClientId("BaiHu")
                                                                                       .WithTcpServer($"{Environment.GetEnvironmentVariable("MQTT_BROKER")}")
                                                                                       .WithCleanSession()
                                                                                       .Build();
    private readonly ILogger<MqttListener> _logger;
    private readonly IMqttClient _mqttClient;
    public MqttListener(ILogger<MqttListener> logger, IMqttClient mqttClient) {
        DotEnv.Load();
        _logger = logger;
        _mqttClient = mqttClient;
        _mqttClient.SubscribeAsync("the-verse/test").ConfigureAwait(false);
        _mqttClient.UseApplicationMessageReceivedHandler(e => {
            _logger.LogInformation("### RECEIVED APPLICATION MESSAGE ###");
            _logger.LogInformation($"+ Topic = {e.ApplicationMessage.Topic}");
            _logger.LogInformation($"+ Payload = {System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            _logger.LogInformation($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
            _logger.LogInformation($"+ Retain = {e.ApplicationMessage.Retain}");
        });
    }
}