using LiteDB;
using ShortUrl.Interfaces.Entities;

namespace ShortUrl.Repositories.Entities
{
	internal class ShortUrlEntity : IShortUrlEntity
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public BsonValue Id { get; set; }

		/// <summary>
		/// Gets or sets the u uid.
		/// </summary>
		/// <value>
		/// The u uid.
		/// </value>
		public string UUid { get; set; }

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
