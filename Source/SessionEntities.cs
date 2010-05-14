using Market;

public class Operator
{
    public string Alpha { get; set; }
    public string Numeric { get; set; }
    public string SimAlpha { get; set; }
    public string SimNumeric { get; set; }

    public Operator(string alpha, string numeric, string simAlpha, string simNumeric)
    {
        Alpha = alpha;
        Numeric = numeric;
        SimAlpha = simAlpha;
        SimNumeric = simNumeric;
    }

    public Operator(string alpha, string numeric) : this(alpha, numeric, alpha, numeric) { }

    public static Operator TMobile = new Operator("T-Mobile", "310260");
    public static Operator SFR = new Operator("F SFR", "20810");
    public static Operator O2 = new Operator("o2 - de", "26207");
    public static Operator Simyo = new Operator("E-Plus", "26203", "simyo", "26203");
    public static Operator Sunrise = new Operator("sunrise", "22802");
}

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
        public const int Version = 1002;
        public const string AndroidId = "0000000000000000";
        public const string DeviceAndSdk = "sapphire:7";
    }

    public class Url
    {
        public const string Login = "https://www.google.com/accounts/ClientLogin";
        public const string ApiRequest = "http://android.clients.google.com/market/api/ApiRequest";
    }
}