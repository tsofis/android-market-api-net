namespace AndroidMarketApi.Entities.Session
{
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
}