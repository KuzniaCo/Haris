using System;
using System.Collections.Generic;
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
using Haris.Core.Services.Luis.Impl;
using Xunit;
using Assert = Xunit.Assert;

namespace Haris.Core.UnitTests._Tests
{
	public class IntentRecognitionTests : TestBase
	{
		protected const string TurnOnTheTvCommand = "turn on the tv";

		private IIntentRecognizer _recognizer;
		private ILuisClient _luisClientMock;
		private ILuisIntentToActionMappingRepository _luisIntentToActionMappingRepoMock;

		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			var turnOnTvFile = File.ReadAllText("TestData/TurnOnTvResponse.txt");
			var turnOnTvIntent = JsonConvert.DeserializeObject<LuisResponseDto>(turnOnTvFile);

			var luisIntentConfig = GetLuisIntentConfig();
			var str = JsonConvert.SerializeObject(luisIntentConfig);
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
			Container.RegisterSingleton<ILuisResponseParser, LuisResponseParser>();
			Container.RegisterSingleton<IIntentToActionConversionService, IntentToActionConversionService>();
			Container.RegisterSingleton<IIntentRecognizer, LuisIntentRecognizer>();
		}

		private CubeConfigDto[] GetLuisIntentConfig()
		{
			return new[]
			{
				new CubeConfigDto
				{
					CubeId = Guid.NewGuid(),
					CubeLabel = "Living room weather station",
					SupportedIntents = new HashSet<IntentLabel> {IntentLabel.Get},
					GetIntentActions = new List<PropertyRelatedIntentDto>
					{
						new PropertyRelatedIntentDto
						{
							IntentLabel = IntentLabel.Get,
							PropertyLabel = "humidity"
						},
						new PropertyRelatedIntentDto
						{
							IntentLabel = IntentLabel.Get,
							PropertyLabel = "temperature"
						}
					}
				},
				new CubeConfigDto
				{
					CubeId = Guid.NewGuid(),
					CubeLabel = "Bedroom TV power control",
					SupportedIntents = new HashSet<IntentLabel> {IntentLabel.TurnOn, IntentLabel.TurnOff},
					TurnOnIntentActions = new List<PowerIntentDto>
					{
						new PowerIntentDto
						{
							IntentLabel = IntentLabel.TurnOn,
							EntityLabel = "tv",
							RoomLabel = "bedroom"
						}
					},
					TurnOffIntentActions = new List<PowerIntentDto>
					{
						new PowerIntentDto
						{
							IntentLabel = IntentLabel.TurnOff,
							EntityLabel = "tv",
							RoomLabel = "bedroom"
						}
					}
				},
				new CubeConfigDto
				{
					CubeId = Guid.NewGuid(),
					CubeLabel = "Kitchen air conditioner",
					SupportedIntents = new HashSet<IntentLabel> {IntentLabel.Set},
					SetIntentActions = new List<PropertyRelatedIntentDto>
					{
						new PropertyRelatedIntentDto
						{
							IntentLabel = IntentLabel.Set,
							RoomLabel = "kitchen",
							PropertyLabel = "temperature"
						}
					}
				}
			};
		}

		public void Init()
		{
			TestFixtureSetUp();
			_recognizer = Container.GetInstance<IIntentRecognizer>();
			_luisClientMock = Container.GetInstance<ILuisClient>();
			_luisIntentToActionMappingRepoMock = Container.GetInstance<ILuisIntentToActionMappingRepository>();
		}

		[Fact]
		public void JsonIsProperlyLoaded()
		{
			var file = File.ReadAllText("TestData/TurnOnTvResponse.txt");

			LuisResponseDto obj = null;
			obj = JsonConvert.DeserializeObject<LuisResponseDto>(file);
			Assert.NotNull(obj);
		}

		[Fact]
		public void IntentRecognizerGetsInitialized()
		{
			Init();
			Assert.NotNull(_recognizer);
		}

		[Fact]
		public async void ResponseIsNotEmpty()
		{
			Init();
			var response = await _recognizer.InterpretIntent(new CommandTextAcquiredEvent(TurnOnTheTvCommand));

			Assert.NotNull(response);
		}

		[Fact]
		public async void LuisGetsAsked()
		{
			Init();
			var response = await _recognizer.InterpretIntent(new CommandTextAcquiredEvent(TurnOnTheTvCommand));

			await _luisClientMock.Received(1).AskLuis(TurnOnTheTvCommand, CancellationToken.None);
		}

		[Fact]
		public void CubesConfigIsRead()
		{
			Init();
			Assert.NotEmpty(_luisIntentToActionMappingRepoMock.CurrentConfig);
		}

		[Fact]
		public void ActionsGetDeserialized()
		{
			var file = File.ReadAllText("TestData/TurnOnTvInBedroomResponseWithActions.txt");
			var response = JsonConvert.DeserializeObject<LuisResponseDto>(file);

			Assert.NotNull(response);
			Assert.Equal(4, response.Intents.Count(i => i.Actions != null && i.Actions.Count > 0));
		}

		[Fact]
		public void TvInBedroomWouldBeTurnedOn()
		{
			Init();
			var file = File.ReadAllText("TestData/TurnOnTvInBedroomResponseWithActions.txt");
			var response = JsonConvert.DeserializeObject<LuisResponseDto>(file);

			var inte = Container.GetInstance<ILuisResponseParser>();
			var intentRecognitionResult = inte.Parse(response);
			var service = Container.GetInstance<IIntentToActionConversionService>();
			var actions = service.GetActions(intentRecognitionResult);
			var action = actions.Single();

			Assert.NotNull(action);
			Assert.Equal("tv", action.EntityLabel);
		}
	}
}