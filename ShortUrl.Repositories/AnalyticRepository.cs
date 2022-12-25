using LiteDB;
using ShortUrl.Interfaces.Entities;
using ShortUrl.Interfaces.Repositories;
using ShortUrl.Repositories.Entities;

namespace ShortUrl.Repositories
{
	/// <summary>
	/// Repository to perform analytic actions within DB layer.
	/// </summary>
	/// <seealso cref="IAnalyticRepository" />
	public class AnalyticRepository : IAnalyticRepository
	{
		/// <summary>
		/// Gets the analytic per host.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		public IEnumerable<IDomainStatisticEntity> GetAnalyticPerHost(string host)
		{
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				var dbShortedData = db.GetCollection<DomainEntity>("rootHosts");
				var result = dbShortedData.FindOne(x => x.RootDomain == host);

				if (result != null)
				{
					var shortUrlsDetail = db.GetCollection<ShortUrlEntity>("shortUrls")
						.Find(x => result.DependentUUIds.Contains(x.UUid));
					var listOfGenLinks = shortUrlsDetail.Select(shortUri =>
							new DomainStatisticEntity
							{
								OriginalUrl = shortUri.OriginalUrl,
								Requested = shortUri.Requested,
								CreatedOn = shortUri.CreatedOn
							})
						.ToList();

					return listOfGenLinks;
				}

				return Enumerable.Empty<IDomainStatisticEntity>();
			}
		}
	}
}
