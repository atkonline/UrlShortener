using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using UrlShortener.Dapper.Dbmodels;
using UrlShortener.Data;

namespace UrlShortener.Dapper
{
    public interface IUrlRepository 
    {
        public Task<string> GetUrlById(int id);
        public Task<string> CreateShortUrl(string longUrl);
        public  Task<string> GetLongUrl(string shortUrl);
    }

    public class UrlRepository : IUrlRepository
    {
        private string _dbConnection { get; }// "Server=DESKTOP-R7ESHCH;Database=UrlShortener;Trusted_Connection=True;";

        private readonly IConfiguration _configuration;
        private UrlHasher _urlHasher { get; set; }

        public UrlRepository(IConfiguration configuration)
        {
            _urlHasher = new UrlHasher();
            _configuration = configuration;
            _dbConnection = _configuration.GetConnectionString("UrlShortenerDb");
        }

        public async Task<string> GetUrlById(int id)
        {
            var sql = $"SELECT Id, LongUrl FROM Url where Id = {id}";

            using (var connection = new SqlConnection(_dbConnection))
            {
                var urlQueryResult = await connection.QuerySingleAsync<Url>(sql);
                var url = urlQueryResult;
           
                return url.LongUrl;
            }
        }

        public async Task<string> CreateShortUrl(string longUrl)
        {
            var insertLongUrl = "INSERT INTO Url (LongUrl, ShortUrl, CreatedDate) VALUES (@LongUrl, null,GETDATE());";
            var idFromLastInsert = "SELECT TOP 1 Id from URL WHERE LongUrl = @LongUrl";

            using (var connection = new SqlConnection(_dbConnection))
            {
                connection.Execute(insertLongUrl, new { LongUrl = longUrl});
                var id = await connection.QuerySingleAsync<int>(idFromLastInsert, new { LongUrl = longUrl });
                var shortUrl = _urlHasher.IntTo36Base(id);
                UpdateUrl(longUrl, shortUrl, id);
                return shortUrl;
            }
        }

        public async Task<string> GetLongUrl(string shortUrl)
        {
            //create an empty row and return Id for url shortening/hashing
            var sql = "SELECT LongUrl FROM Url WHERE ShortUrl = @ShortUrl;";
           
            using (var connection = new SqlConnection(_dbConnection))
            {
                var result = await connection.QuerySingleAsync<Url>(sql, new { ShortUrl = shortUrl});
                return result.LongUrl;
            }
        }

        public async void UpdateUrl (string longUrl,string shortUrl, int id)
        {
            var sql = "UPDATE Url SET LongUrl = @LongUrl, ShortUrl = @ShortUrl WHERE Id = @Id;";

            using (var connection = new SqlConnection(_dbConnection))
            {
                await connection.ExecuteAsync(sql, new { LongUrl = longUrl, ShortUrl = shortUrl, Id = id});
            }
        }
    }
}
