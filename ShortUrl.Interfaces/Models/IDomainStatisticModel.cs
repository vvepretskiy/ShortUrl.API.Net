namespace ShortUrl.Interfaces.Models
{
	public interface IDomainStatisticModel
	{
		/// <summary>
		/// Gets or sets the created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		public DateTime CreatedOn { get; }

		/// <summary>
		/// Gets or sets the requested (the amount short link been requested).
		/// </summary>
		/// <value>
		/// The requested.
		/// </value>
		public int Requested { get; }

		/// <summary>
		/// Gets or sets the original URL.
		/// </summary>
		/// <value>
		/// The original URL.
		/// </value>
		public Uri OriginalUrl { get; }
	}
}
