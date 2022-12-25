using ShortUrl.Interfaces.Models;

namespace ShortUrl.Services.Models
{
	internal record ShortUrlModel(Uri OriginalUrl) : IShortUrlModel
	{
	}
}
