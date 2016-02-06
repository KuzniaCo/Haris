namespace Haris.Core.Events.Command
{
	public class CommandTextAcquiredEvent: BaseEvent<string>
	{
		public CommandTextAcquiredEvent(string cmd): base(cmd)
		{
		}
	}
}