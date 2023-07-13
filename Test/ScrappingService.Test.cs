using NUnit.Framework;


namespace Test.Services
{
    [TestFixture]
    public class ScrappingService_IsCountNumber
    {
        private ScrappingService _scrappingService;

        [SetUp]
        public void SetUp()
        {
           _scrappingService = new ScrappingService("https://www.occ.com.mx");
        }

        [Test]
        async public Task IsCountNumber()
        {
          var result = await _scrappingService.GetResults("cocaCola");

          Assert.Greater(result, 0);
        }
    }
}
