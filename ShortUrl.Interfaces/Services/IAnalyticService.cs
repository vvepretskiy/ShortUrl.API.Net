using ShortUrl.Interfaces.Models;

namespace ShortUrl.Interfaces.Services
{
	/// <summary>
	/// Analytic Service interface
	/// </summary>
	public interface IAnalyticService
	{
		/// <summary>
		/// Gets the domain statistic asynchronous.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns cref="IDomainStatisticModel">List domain statistic</returns>
		public Task<IEnumerable<IDomainStatisticModel>> GetDomainStatisticAsync(string host);
	}
}
