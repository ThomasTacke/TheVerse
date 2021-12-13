using dotenv.net;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;

namespace BaiHu.Services;

public class MqttClientService : IMqttClientConnectedHandler, IMqttClientDisconnectedHandler, IMqttApplicationMessageReceivedHandler {
    public static IMqttClientOptions MqttClientOptions = new MqttClientOptionsBuilder().WithClientId("BaiHu")
                                                                                       .WithTcpServer($"{Environment.GetEnvironmentVariable("MQTT_BROKER")}")
                                                                                       .WithCleanSession()
                                                                                       .Build();
    private readonly ILogger<MqttClientService> _logger;
    private readonly IMqttClient _mqttClient;
    public MqttClientService(ILogger<MqttClientService> logger) {
        DotEnv.Load();
        _logger = logger;
        _mqttClient = new MqttFactory().CreateMqttClient();
        ConfigureMqttClient();
        if (!_mqttClient.IsConnected)
            _mqttClient.ConnectAsync(MqttClientOptions);
    }
    private void ConfigureMqttClient() {
        _mqttClient.ConnectedHandler = this;
        _mqttClient.DisconnectedHandler = this;
        _mqttClient.ApplicationMessageReceivedHandler = this;
    }
    public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs) {
        _logger.LogInformation($"Connected to broker! Start subs.");
        await _mqttClient.SubscribeAsync("the-verse/+/+/light").ConfigureAwait(false);
    }

    public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs) {
        await Task.Delay(TimeSpan.FromSeconds(5));
        try {
            await _mqttClient.ReconnectAsync();
        } catch (MQTTnet.Exceptions.MqttCommunicationTimedOutException) { }
    }

    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs) {
        switch (eventArgs.ApplicationMessage.Topic) {
            case "the-verse/office/pc/light":
                _logger.LogInformation(eventArgs.ApplicationMessage.ConvertPayloadToString());
                break;
            default:
                _logger.LogInformation($"No handler for topic {eventArgs.ApplicationMessage.Topic}");
                break;
        }
    }
}
