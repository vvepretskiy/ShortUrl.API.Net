using System.Collections.Generic;
using ShortUrl.API.Models;

namespace ShortUrl.API.Repositories.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IUrlsRepository
	{
		/// <summary>
		/// Gets the short URL data.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns></returns>
		Models.ShortUrl GetShortUrlData(string uuid);

		/// <summary>
		/// Gets the analytic per host.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		IEnumerable<GeneratedLinks> GetAnalyticPerHost(string host);

		/// <summary>
		/// Adds the short URL.
		/// </summary>
		/// <param name="shortUrl">The short URL.</param>
		void AddShortUrl(Models.ShortUrl shortUrl);
	}
}