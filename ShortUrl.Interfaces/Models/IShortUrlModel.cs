using System;
namespace ShortUrl.Interfaces.Models
{
	public interface IShortUrlModel
	{
		/// <summary>
		/// Gets the URL.
		/// </summary>
		/// <value>
		/// The original URL.
		/// </value>
		public Uri OriginalUrl { get; }
	}
}
