using UrlShortener.Dapper;
using Xunit;
using FluentAssertions;

namespace UrlShortener.Tests.Data
{
    public class DapperQueryTests
    {
        [Fact]
        public void TestDbConnection()
        {
            //var db = new UrlRepository();
            //var FirstRowIdInDatabase = 1000000;
            //var result = db.GetUrlById(FirstRowIdInDatabase).Result;
            //result.Should().Be("www.google.com");
        }

        [Fact]
        public void AddBlankUrlEntry_Test()
        {
        }
    }
}
