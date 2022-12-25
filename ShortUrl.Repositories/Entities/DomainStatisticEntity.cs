using ShortUrl.Interfaces.Entities;

namespace ShortUrl.Repositories.Entities
{
	public class DomainStatisticEntity : IDomainStatisticEntity
	{
		/// <summary>
		/// Gets or sets the created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the requested (the amount short link been requested).
		/// </summary>
		/// <value>
		/// The requested.
		/// </value>
		public int Requested { get; set; }

		/// <summary>
		/// Gets or sets the original URL.
		/// </summary>
		/// <value>
		/// The original URL.
		/// </value>
		public Uri OriginalUrl { get; set; }
	}
}
