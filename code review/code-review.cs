internal class Ping {
  public string Timestamp {get;set;}
  public string UserId {get;set;}
}


internal class MyCommunicator {

  public static IMyDatabase MyClient {get; set;} =
    new MyDatabase(
      new MyConnectionSettings(
        "Compress=True;CheckCompressedHash=False;Compressor=lz4;Host=192.168.0.163;Port=9000;User=admin;Password=qwerty;SocketTimeout=600000;Database=TeasersStat;"),
      new MyCommandFormatter(),
      new MyConnectionFactory(),
      null,
      new DefaultPropertyBinder()
    );

  public static List<Ping> PingsCache {private set; get;} = new List<Ping>();
  private static object obj = new object;

  public static void SendData() {
    if(lock(obj)){
        var request = "INSERT INTO pings VALUES ";
        var isFirst = true;
    
        StringBuilder request = new StringBuilder();
        foreach (var ping in PingsCache) {
          if (!isFirst) {
            request += ",";
          }
    
          request += $"('{ping.Timestamp}', '{ping.UserId}')";
    
          isFirst = false;
        }
        
        MyClient.ExecuteNonQuery(request);
    
        PingsCache = new List<Ping>();   
    }
  }
}


internal class TimedSendDataService : IHostedService
{
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void SendData(object state) {MyCommunicator.SendData();}
}


[Route("api/[controller]")]
[ApiController]
public class PingsController : ControllerBase
{
  [HttpPost]
  public async bool PostPing(Ping ping)
  {
      MyCommunicator.PingsCache.Add(ping);

      if (MyCommunicator.PingsCache.Length > 50) {
        MyCommunicator.SendData();
      }

      return true;
  }
}