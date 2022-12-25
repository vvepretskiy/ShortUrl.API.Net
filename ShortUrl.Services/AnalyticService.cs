using ShortUrl.Interfaces.Services;
using Microsoft.Extensions.Logging;
using ShortUrl.Interfaces.Repositories;
using ShortUrl.Interfaces.Models;
using ShortUrl.Services.Models;

namespace ShortUrl.Services
{
	/// <summary>
	/// Analytic Service contains all the business logic.
	/// Logic should be placed here, not in Controllers.
	/// </summary>
	/// <seealso cref="IAnalyticService" />
	public class AnalyticService : IAnalyticService
	{
		/// <summary>
		/// The logger
		/// </summary>
		private readonly ILogger<AnalyticService> _logger;

		/// <summary>
		/// The urls repository
		/// </summary>
		private readonly IAnalyticRepository _analyticRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="AnalyticService"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="analyticRepository">The analytic repository.</param>
		public AnalyticService(ILogger<AnalyticService> logger, IAnalyticRepository analyticRepository)
		{
			_logger = logger;
			_analyticRepository = analyticRepository;
		}

		/// <summary>
		/// Gets the domain statistic asynchronous.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		public async Task<IEnumerable<IDomainStatisticModel>> GetDomainStatisticAsync(string host)
		{
			try
			{
				var hostLinks = _analyticRepository.GetAnalyticPerHost(host);

				if (hostLinks != null)
				{
					var data = hostLinks.Select(s => new DomainStatisticModel(s.OriginalUrl, s.Requested, s.CreatedOn));
					return await Task.FromResult(data);
				}

				return Enumerable.Empty<IDomainStatisticModel>();
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				throw;
			}
		}
	}
}
