using ShortUrl.Interfaces.Services;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using ShortUrl.Interfaces.Repositories;
using ShortUrl.Interfaces.Models;
using ShortUrl.Services.Models;

namespace ShortUrl.Services
{
	/// <summary>
	/// Url Service contains all the business logic.
	/// Logic should be placed here, not in Controllers.
	/// </summary>
	/// <seealso cref="IUrlService" />
	public class UrlService : IUrlService
	{
		/// <summary>
		/// The URL regex
		/// </summary>
		private readonly Regex _urlRegex = new Regex("(ftp|http|https)?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()!@:%_\\+.~#?&\\/\\/=]*)");

		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<UrlService> _logger;

		/// <summary>
		/// The urls repository
		/// </summary>
		private readonly IUrlRepository _urlRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlService"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="urlRepository">The url repository.</param>
		public UrlService(ILogger<UrlService> logger, IUrlRepository urlRepository)
		{
			_logger = logger;
			_urlRepository = urlRepository;
		}

		/// <summary>
		/// Gets the short URL asynchronous.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns></returns>
		public async Task<IShortUrlModel> GetUrlAsync(string uuid)
		{
			try
			{
				var entity = _urlRepository.GetShortUrlData(uuid);
				if (entity == null)
				{
					throw new EntryPointNotFoundException();
				}
				return await Task.FromResult<IShortUrlModel>(new ShortUrlModel(entity.OriginalUrl));
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				throw;
			}
		}

		/// <summary>
		/// Modify URL to matched view.
		/// </summary>
		/// <param name="url">Original url</param>
		/// <returns>Url</returns>
		private Uri GenerateUri(Uri url)
		{
			var someRegex = new Regex("^(ftp|http|https)://[^ \"]+$");
			var linkToStr = url.ToString();
			if (!someRegex.IsMatch(linkToStr))
			{
				var trimmer = new Regex(@"\s");
				return new Uri($"http://{trimmer.Replace(linkToStr, "")}");
			}
			return url;
		}

		/// <summary>
		/// Check URL.
		/// </summary>
		/// <param name="url">The original url.</param>
		/// <returns></returns>
		public bool isValidUrl(Uri url)
		{
			return _urlRegex.IsMatch(url.ToString());
		}

		/// <summary>
		/// Adds the short URL asynchronous.
		/// </summary>
		/// <param name="url">The original url.</param>
		/// <returns></returns>
		public async Task<string> AddUrlAsync(Uri url)
		{
			try
			{
				var link = GenerateUri(url);
				var uuid = await Nanoid.Nanoid.GenerateAsync(size: 7);
				_urlRepository.AddShortUrl(uuid, link);
				return uuid;
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				throw;
			}
		}
	}
}
