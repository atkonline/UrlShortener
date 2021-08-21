using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models;
using System.Diagnostics;
using UrlShortener.Dapper;
using System.Threading.Tasks;

namespace UrlShortener.Controllers
{
    public class UrlShortenerController : Controller
    {
        private readonly ILogger<UrlShortenerController> _logger;
        private IUrlRepository _urlRepository;


        public UrlShortenerController(ILogger<UrlShortenerController> logger, IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //validateUrl
            //handleURLShortening
            //ReturnView
            return View("Index", new UrlShortenerViewModel { LongUrl = "", ShortUrl = "" });
        }

        [HttpPost]
        public async Task<ViewResult> Index(UrlShortenerViewModel urlshortener)
        {
            //validateUrl
            //handleURLShortening
            //ReturnView
            //var urlValidator = new UrlValidator();
            var shortenedUrl = await _urlRepository.CreateShortUrl(urlshortener.LongUrl);

            return View("Index", new UrlShortenerViewModel { LongUrl = urlshortener.LongUrl, ShortUrl = shortenedUrl });
        }

        [HttpGet]
        [Route("{shortUrl:regex([[a-z0-9]]$):maxlength(8)}")] //[[a-z]]//|[[0-9]]){{3,8}}
        public async Task<RedirectResult> RedirectShortUrl(string shortUrl)
        {
            var url = await _urlRepository.GetLongUrl(shortUrl.ToLower());
            return Redirect(url);
        }


        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
