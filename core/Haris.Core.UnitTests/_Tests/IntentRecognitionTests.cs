using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.Core.Modules.IntentRecognition.Core;
using Haris.Core.Services.Luis;
using Haris.DataModel.IntentRecognition;
using Haris.DataModel.Luis;
using Newtonsoft.Json;
using NSubstitute;
using System.Linq;
using NUnit.Framework;

namespace Haris.Core.UnitTests._Tests
{
	[TestFixture]
	public class IntentRecognitionTests: TestBase
	{
		protected const string TurnOnTheTvCommand = "turn on the tv";

		private IIntentRecognizer _recognizer;
		private ILuisClient _luisClientMock;
		private ILuisIntentToActionMappingRepository _luisIntentToActionMappingRepoMock;

		[TestFixtureSetUp]
		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			var turnOnTvFile = File.ReadAllText("TestData/TurnOnTvResponse.txt");
			var turnOnTvIntent = JsonConvert.DeserializeObject<LuisResponseDto>(turnOnTvFile);

			var luisIntentConfigFile = File.ReadAllText("TestData/CubesConfig.txt");
			var luisIntentConfig = JsonConvert.DeserializeObject<CubeConfigDto[]>(luisIntentConfigFile);
			var luisClientMock = Substitute.For<ILuisClient>();
			luisClientMock.AskLuis("", CancellationToken.None).ReturnsForAnyArgs(info =>
			{
				switch (info.Arg<string>())
				{
					case TurnOnTheTvCommand:
						return Task.FromResult(turnOnTvIntent);
				}
				return Task.FromResult<LuisResponseDto>(null);
			});

			var luisIntentToActionMappingRepoMock = Substitute.For<ILuisIntentToActionMappingRepository>();
			luisIntentToActionMappingRepoMock.CurrentConfig.Returns(luisIntentConfig);

			Container.RegisterSingleton(luisClientMock);
			Container.RegisterSingleton(luisIntentToActionMappingRepoMock);
			Container.RegisterSingleton<IIntentToActionConversionService, IntentToActionConversionService>();
			Container.RegisterSingleton<IIntentRecognizer, LuisIntentRecognizer>();
		}

		[SetUp]
		public void Init()
		{
			_recognizer = Container.GetInstance<IIntentRecognizer>();
			_luisClientMock = Container.GetInstance<ILuisClient>();
			_luisIntentToActionMappingRepoMock = Container.GetInstance<ILuisIntentToActionMappingRepository>();
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

		[Test]
		public void CubesConfigIsRead()
		{
			Assert.IsNotEmpty(_luisIntentToActionMappingRepoMock.CurrentConfig);
		}

		[Test]
		public void ActionsGetDeserialized()
		{
			var file = File.ReadAllText("TestData/TurnOffTvInBedroomResponseWithActions.txt");
			var response = JsonConvert.DeserializeObject<LuisResponseDto>(file);

			Assert.That(response, Is.Not.Null);
			Assert.AreEqual(2, response.Intents.Count(i => i.Actions != null && i.Actions.Count > 0));
		}
	}
}