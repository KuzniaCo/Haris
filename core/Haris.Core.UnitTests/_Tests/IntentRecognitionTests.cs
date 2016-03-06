using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.Core.Services.Luis;
using Haris.DataModel.Luis;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace Haris.Core.UnitTests._Tests
{
	[TestFixture]
	public class IntentRecognitionTests
	{
		private LuisIntentRecognizer _recognizer;
		private ILuisClient _luisClientMock;
		private const string TurnOnTheTvCommand = "turn on the tv";

		[SetUp]
		public void Init()
		{
			var turnOnTvFile = File.ReadAllText("TestData/TurnOnTvResponse.txt");
			var turnOnTvIntent = JsonConvert.DeserializeObject<LuisResponseDto>(turnOnTvFile);

			_luisClientMock = Substitute.For<ILuisClient>();
			_luisClientMock.AskLuis("", CancellationToken.None).Returns(info =>
			{
				switch (info.Arg<string>())
				{
					case TurnOnTheTvCommand:
						return Task.FromResult(turnOnTvIntent);
				}
				return Task.FromResult<LuisResponseDto>(null);
			});

			_recognizer = new LuisIntentRecognizer(_luisClientMock);
		}

		[Test]
		public void JsonIsProperlyLoaded()
		{
			var file = File.ReadAllText("TestData/TurnOnTvResponse.txt");

			LuisResponseDto obj = null;
			Assert.DoesNotThrow(() => obj = JsonConvert.DeserializeObject<LuisResponseDto>(file));
			Assert.That(obj, Is.Not.Null);
		}

		[Test]
		public void IntentRecognizerGetsInitialized()
		{
			Assert.That(_recognizer, Is.Not.Null);
		}

		[Test]
		public async void ResponseIsNotEmpty()
		{
			var response = await _recognizer.InterpretIntent(new CommandTextAcquiredEvent(TurnOnTheTvCommand));

			Assert.IsNotEmpty(response);
		}

		[Test]
		public async void LuisGetsAsked()
		{
			var response = await _recognizer.InterpretIntent(new CommandTextAcquiredEvent(TurnOnTheTvCommand));

			await _luisClientMock.Received(1).AskLuis(TurnOnTheTvCommand, CancellationToken.None);
		}
	}
}