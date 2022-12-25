using ShortUrl.Interfaces.Models;

namespace ShortUrl.Interfaces.Services
{
	/// <summary>
	/// Url Service interface
	/// </summary>
	public interface IUrlService
	{
		/// <summary>
		/// Gets the short URL asynchronous.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns cref="IShortUrlModel">Returns short url details per uuid</returns>
		Task<IShortUrlModel> GetUrlAsync(string uuid);

		/// <summary>
		/// Validation URL.
		/// </summary>
		/// <param name="url">The original url.</param>
		/// <returns>isValid</returns>
		public bool isValidUrl(Uri url);

		/// <summary>
		/// Adds the short URL asynchronous.
		/// </summary>
		/// <param name="url">The original url.</param>
		/// <returns>uuid for generate link</returns>
		public Task<string> AddUrlAsync(Uri url);
	}
}
