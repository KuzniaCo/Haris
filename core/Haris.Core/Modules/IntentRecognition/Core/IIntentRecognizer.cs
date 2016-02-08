using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Haris.Core.Events.Command;
using Haris.DataModel.Action;

namespace Haris.Core.Modules.IntentRecognition.Core
{
	public interface IIntentRecognizer
	{
		Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt);
		Task<IReadOnlyCollection<ActionDescriptorDto>> InterpretIntent(CommandTextAcquiredEvent evt, CancellationToken ct);
	}
}