using System.Configuration;
using AndroidMarketApi.Entities.protobuf;
using NUnit.Framework;

namespace AndroidMarketApi.Tests
{
  [TestFixture]
  public class BasicFunctionalutyTests
  {
    private string email;
    private string password;

    private MarketSession session;

    [TestFixtureSetUp]
    public void TestFixtureSetUp()
    {
      email = ConfigurationManager.AppSettings["email"];
      password = ConfigurationManager.AppSettings["password"];
    }

    [SetUp]
    public void Setup()
    {
      session = new MarketSession();

      session.Login(email, password);
    }

    [TearDown]
    public void TearDown()
    {
      session = null;
      email = null;
      password = null;
    }


    [Test]
    public void AppsRequest()
    {
      string query = "maps";
      AppsRequest appsRequest = new AppsRequest
                                  {
                                    Query = query, StartIndex = 0, EntriesCount = 10, WithExtendedInfo = true
                                  };

      var response = session.Get(appsRequest);
    }

    [Test]
    public void CategoriesRequest()
    {
      CategoriesRequest categoriesRequest = new CategoriesRequest();

      var response = session.Get(categoriesRequest);
    }
  }
}