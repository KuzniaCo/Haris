using System.IO;
using System.Threading;
using Haris.DataModel.IntentRecognition;
using Newtonsoft.Json;

namespace Haris.Core.Services.Luis
{
	public interface ILuisIntentToActionMappingRepository
	{
		CubeConfigDto[] CurrentConfig { get; }
		void ModifyConfig(CubeConfigDto[] config);
	}

	public class LuisIntentToActionMappingRepository : ILuisIntentToActionMappingRepository
	{
		private const string ConfigFileName = @"Config\intentsToActions.json";
		private CubeConfigDto[] _config;
		private readonly ReaderWriterLockSlim _rw;

		public LuisIntentToActionMappingRepository()
		{
			_rw = new ReaderWriterLockSlim();
			if (File.Exists(ConfigFileName) == false)
			{
				throw new InvalidDataException("LUIS intents config does not exist");
			}
		}

		private CubeConfigDto[] ReadConfig()
		{
			var file = File.ReadAllText(ConfigFileName);

			return JsonConvert.DeserializeObject<CubeConfigDto[]>(file, new Newtonsoft.Json.Converters.StringEnumConverter());
		}

		private void SaveConfig()
		{
			var contents = JsonConvert.SerializeObject(_config);
			File.WriteAllText(ConfigFileName, contents);
		}

		public CubeConfigDto[] CurrentConfig
		{
			get
			{
				_rw.EnterReadLock();
				if (_config == null)
				{
					_config = ReadConfig();
				}
				var result = _config;
				_rw.ExitReadLock();
				return result;
			}
		}

		public void ModifyConfig(CubeConfigDto[] config)
		{
			_rw.EnterWriteLock();
			_config = config;
			SaveConfig();
			_rw.ExitWriteLock();
		}
	}
}