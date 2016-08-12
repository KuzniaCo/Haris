using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Services.Luis
{
	public interface ILuisIntentToActionMappingRepository
	{
		CubeConfigDto[] CurrentConfig { get; }
		void ModifyConfig(CubeConfigDto[] config);
	}
}