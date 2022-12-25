using ShortUrl.Interfaces.Models;

namespace ShortUrl.Services.Models
{
	internal record DomainStatisticModel(Uri OriginalUrl, int Requested, DateTime CreatedOn) : IDomainStatisticModel
	{
	}
}
