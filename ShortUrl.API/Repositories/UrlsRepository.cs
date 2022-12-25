using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using ShortUrl.API.Models;
using ShortUrl.API.Repositories.Interfaces;

namespace ShortUrl.API.Repositories
{
	/// <summary>
	/// Repository to perform all the actions within DB layer.
	/// </summary>
	/// <seealso cref="ShortUrl.API.Repositories.Interfaces.IUrlsRepository" />
	/// <seealso cref="IUrlsRepository" />
	public class UrlsRepository : IUrlsRepository
	{
		/// <summary>
		/// Gets the short URL data.
		/// </summary>
		/// <param name="uuid">The UUID.</param>
		/// <returns></returns>
		public Models.ShortUrl GetShortUrlData(string uuid)
		{
			Models.ShortUrl shortUrl;
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				var dbStructure = db.GetCollection<Models.ShortUrl>("shortUrls");
				shortUrl = dbStructure.FindOne(x => x.UUid == uuid);

				if (shortUrl != null)
				{
					shortUrl.Requested++;
					dbStructure.Update(shortUrl);
				}
			}

			return shortUrl;
		}

		/// <summary>
		/// Gets the analytic per host.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		public IEnumerable<GeneratedLinks> GetAnalyticPerHost(string host)
		{
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				var dbShortedData = db.GetCollection<DomainData>("rootHosts");
				var result = dbShortedData.FindOne(x => x.RootDomain == host);

				if (result != null)
				{
					var shortUrlsDetail = db.GetCollection<Models.ShortUrl>("shortUrls")
						.Find(x => result.DependentUUIds.Contains(x.UUid));
					var listOfGenLinks = shortUrlsDetail.Select(shortUri =>
							new GeneratedLinks(shortUri.OriginalUrl, shortUri.Requested,
								shortUri.CreatedOn))
						.ToList();

					return listOfGenLinks;
				}

				return null;
			}
		}

		/// <summary>
		/// Adds the short URL.
		/// </summary>
		/// <param name="shortUrl">The short URL.</param>
		public void AddShortUrl(Models.ShortUrl shortUrl)
		{
			using (var db = new LiteDatabase(@"DbStorage/LocalDb.db"))
			{
				// Get customer collection
				var col = db.GetCollection<Models.ShortUrl>("shortUrls");
				col.Insert(shortUrl);

				// Will also add some analytic per host.
				// Each host will have an array of uuids generated per link
				var dbShortedData = db.GetCollection<DomainData>("rootHosts");

				DomainData domainObject =
					dbShortedData.FindOne(x => x.RootDomain == shortUrl.OriginalUrl.Host);

				if (domainObject != null)
				{
					domainObject.DependentUUIds.Add(shortUrl.UUid);
					dbShortedData.Update(domainObject);
				}
				else
				{
					domainObject = new DomainData
					{
						Id = new BsonValue(Guid.NewGuid()),
						RootDomain = shortUrl.OriginalUrl.Host,
						DependentUUIds = new List<string>
						{
							shortUrl.UUid
						}
					};
					dbShortedData.Insert(domainObject);
				}
			}
		}
	}
}