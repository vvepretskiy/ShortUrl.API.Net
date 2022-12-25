using LiteDB;

namespace ShortUrl.Repositories.Entities
{
	/// <summary>
	/// Domain data
	/// </summary>
	public class DomainEntity
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public BsonValue Id { get; set; }

		/// <summary>
		/// Gets or sets the root domain.
		/// </summary>
		/// <value>
		/// The root domain.
		/// </value>
		public string RootDomain { get; set; }

		/// <summary>
		/// Gets or sets the dependent uu ids.
		/// </summary>
		/// <value>
		/// The dependent uu ids.
		/// </value>
		public List<string> DependentUUIds { get; set; }
	}
}
