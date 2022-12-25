using System;

namespace ShortUrl.API.Dto
{
	/// <summary>
	/// Request object to generate short link for
	/// </summary>
	public class ShortUrlRequestDto
	{
		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>
		/// The link.
		/// </value>
		public Uri Link { get; set; }
	}
}