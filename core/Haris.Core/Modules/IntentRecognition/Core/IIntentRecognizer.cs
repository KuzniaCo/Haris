using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.DataModel.IntentRecognition;

namespace Haris.Core.Modules.IntentRecognition.Core
{
	public interface IIntentRecognizer
	{
		Task<IntentRecognitionResult> InterpretIntent(CommandTextAcquiredEvent evt);
		Task<IntentRecognitionResult> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct);
	}
}