namespace ShortUrl.Interfaces.Entities
{
	public interface IShortUrlEntity
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
