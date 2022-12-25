using ShortUrl.Interfaces.Entities;

namespace ShortUrl.Interfaces.Repositories
{
	public interface IAnalyticRepository
	{
		/// <summary>
		/// Gets the analytic per host.
		/// </summary>
		/// <param name="host">The host.</param>
		/// <returns></returns>
		IEnumerable<IDomainStatisticEntity> GetAnalyticPerHost(string host);
	}
}
