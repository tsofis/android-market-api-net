namespace AndroidMarketApi.Entities.Session
{
  public class Constant
  {
    public class AccountType
    {
      public const string Hosted = "HOSTED";
      public const string Google = "GOOGLE";
      public const string HostedOrGoogle = "HOSTED_OR_GOOGLE";
    }

    public class Config
    {
      public const string Service = "android";
      public const int ProtocolVersion = 2;
    }

    public class DefaultContext
    {
      public const int Unknown1 = 0;
      public const int Version = 2009011;
      public const string AndroidId = "0123456789123456";
      public const string DeviceAndSdk = "passion:9";
    }

    public class Url
    {
      public const string Login = "https://www.google.com/accounts/ClientLogin";
      public const string ApiRequest = "http://android.clients.google.com/market/api/ApiRequest";
    }
  }
}