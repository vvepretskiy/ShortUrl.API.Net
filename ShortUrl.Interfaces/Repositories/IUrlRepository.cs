using ShortUrl.Interfaces.Entities;

namespace ShortUrl.Interfaces.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public interface IUrlRepository
	{
		/// <summary>
		/// Gets the short URL data.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns></returns>
		IShortUrlEntity? GetShortUrlData(string uuid);

		/// <summary>
		/// Adds the short URL.
		/// </summary>
		/// <param name="uuid">The hash id URL.</param>
		/// <param name="url">The original URL.</param>
		void AddShortUrl(string uuid, Uri url);
	}
}