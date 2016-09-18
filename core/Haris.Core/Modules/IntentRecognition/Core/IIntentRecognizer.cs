using Haris.Core.Events.Command;
using Haris.DataModel.IntentRecognition;
using System.Threading;
using System.Threading.Tasks;

namespace Haris.Core.Modules.IntentRecognition.Core
{
	public interface IIntentRecognizer
	{
		Task<IntentRecognitionResultDto> InterpretIntent(CommandTextAcquiredEvent evt);
		Task<IntentRecognitionResultDto> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct);
	}
}