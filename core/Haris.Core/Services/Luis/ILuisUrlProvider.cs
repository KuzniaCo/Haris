namespace Haris.Core.Services.Luis
{
	public interface ILuisUrlProvider
	{
		string GetUrlForQuery(string query);
		string BaseUrl { get; }
	}
}