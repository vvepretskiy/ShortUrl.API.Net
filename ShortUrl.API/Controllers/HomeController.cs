using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShortUrl.API.Dto;
using ShortUrl.Interfaces.Services;

namespace ShortUrl.API.Controllers
{
	/// <summary>
	/// Main APi controller
	/// </summary>
	/// <seealso cref="ControllerBase" />
	[ApiController]
	[Route("api")]
	public class HomeController : ControllerBase
	{
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<HomeController> _logger;

		/// <summary>
		/// The url service
		/// </summary>
		private readonly IUrlService _urlService;

		/// <summary>
		/// The analytic service
		/// </summary>
		private readonly IAnalyticService _analyticService;

		/// <summary>
		/// Initializes a new instance of the <see cref="HomeController"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="shortUrlsService">The short urls service.</param>
		public HomeController(ILogger<HomeController> logger, IUrlService urlService, IAnalyticService analyticService)
		{
			_logger = logger;
			_urlService = urlService;
			_analyticService = analyticService;
		}

		/// <summary>
		/// Ping
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Ping-Pong");
		}

		/// <summary>
		/// Gets the shorten URL data asynchronous.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns cref="Models.ShortUrl">Short URL detail object</returns>
		[HttpGet("{uuid}")]
		[ActionName("GetShortData")]
		public async Task<IActionResult> GetShortenUrlDataAsync([FromRoute] string uuid)
		{
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug($"Begin getting Url data for uuid: {uuid}");
			}

			try
			{
				var response = await _urlService.GetUrlAsync(uuid);
				return Ok(new { originalUrl = response.OriginalUrl });
			}
			catch
			{
				_logger.LogError($"No uuid: {uuid} data has been found.");
				return NotFound($"There are no data for {uuid}");
			}
		}

		/// <summary>
		/// Generates the short link asynchronous.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>Dynamic object with uuid property only</returns>
		[HttpPost]
		public async Task<IActionResult> GenerateShortLinkAsync(ShortUrlRequestDto request)
		{
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug("Begin Short Link Generation");
			}


			if (!_urlService.isValidUrl(request.Link))
			{
				return BadRequest("Please provide correct URL");
			}

			try
			{
				var resp = await _urlService.AddUrlAsync(request.Link);
				return Ok(new { uuid = resp });
			}
			catch
			{
				_logger.LogError($"Unable to generate short link for the request: {JsonConvert.SerializeObject(request)} ");
				return BadRequest("Unable to proceed the request, please contact System Administrator");
			}
		}

		/// <summary>
		/// Gets the analytic asynchronous.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns cref="DomainStatisticResponse">Statistic per domain (f.e. www.google.com or github.com all the links generated will be here)</returns>
		[HttpGet("analytic")]
		[ActionName("GatAnalyticData")]
		public async Task<IActionResult> GetAnalyticAsync([FromQuery] string host)
		{
			if (_logger.IsEnabled(LogLevel.Debug))
			{
				_logger.LogDebug($"Begin retrieving analytic for the host: {host}");
			}

			try
			{
				var result = await _analyticService.GetDomainStatisticAsync(host);
				return Ok(new
				{
					Host = host,
					GeneratedShortUrls = result,
				});
			}
			catch
			{
				_logger.LogError($"No analytic has been found for the host: {host}");
				return NotFound($"There are no analytic data found for {host}");
			}
		}
	}
}
