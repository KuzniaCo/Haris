﻿using System.Net;

namespace Haris.Core.Services.Luis
{
	public interface ILuisUrlProvider
	{
		string GetUrlForQuery(string query);
		string BaseUrl { get; }
	}

	public class LuisUrlProvider : ILuisUrlProvider
	{
		private const string _baseUrl = "https://api.projectoxford.ai/luis/v1/";
		public const string LuisUrlFormat =
			"application?id={0}&subscription-key={1}&q={2}";

		public const string LuisAppId = "fe289a65-018e-4553-8765-a837d348fe63";
		public const string LuisSubscriptionKey = "62cdb9e662d841b7857f3cd1c00185e7";

		public string BaseUrl
		{
			get { return _baseUrl; }
		}

		public string GetUrlForQuery(string query)
		{
			return string.Format(LuisUrlFormat, LuisAppId, LuisSubscriptionKey, WebUtility.UrlEncode(query));
		}
	}
}