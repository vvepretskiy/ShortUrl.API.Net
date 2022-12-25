using LiteDB;
using ShortUrl.Interfaces.Entities;
using ShortUrl.Interfaces.Repositories;
using ShortUrl.Repositories.Entities;

namespace ShortUrl.Repositories
{
	/// <summary>
	/// Repository to perform all the actions within DB layer.
	/// </summary>
	/// <seealso cref="IUrlRepository" />
	public class UrlRepository : IUrlRepository
	{
		/// <summary>
		/// Adds the short URL.
		/// </summary>
		/// <param name="shortUrl">The short URL.</param>
		public void AddShortUrl(string uuid, Uri url)
		{
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				var enity = new ShortUrlEntity
				{
					Id = new BsonValue(Guid.NewGuid()),
					CreatedOn = DateTime.Now,
					UUid = uuid,
					OriginalUrl = url,
					Requested = 0
				};

				// Get customer collection
				var col = db.GetCollection<ShortUrlEntity>("shortUrls");
				col.Insert(enity);

				// Will also add some analytic per host.
				// Each host will have an array of uuids generated per link
				var dbShortedData = db.GetCollection<DomainEntity>("rootHosts");

				DomainEntity domainObject =
					dbShortedData.FindOne(x => x.RootDomain == enity.OriginalUrl.Host);

				if (domainObject != null)
				{
					domainObject.DependentUUIds.Add(enity.UUid);
					dbShortedData.Update(domainObject);
				}
				else
				{
					domainObject = new DomainEntity
					{
						Id = new BsonValue(Guid.NewGuid()),
						RootDomain = enity.OriginalUrl.Host,
						DependentUUIds = new List<string>
						{
							enity.UUid
						}
					};
					dbShortedData.Insert(domainObject);
				}
			}
		}

		/// <summary>
		/// Gets the short URL data.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns></returns>
		public IShortUrlEntity? GetShortUrlData(string uuid)
		{
			ShortUrlEntity shortUrl;
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				var dbStructure = db.GetCollection<ShortUrlEntity>("shortUrls");
				shortUrl = dbStructure.FindOne(x => x.UUid == uuid);

				if (shortUrl != null)
				{
					shortUrl.Requested++;
					dbStructure.Update(shortUrl);
				}
			}

			return shortUrl;
		}
	}
}