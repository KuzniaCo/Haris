using System;
using Caliburn.Micro;
using Haris.Core.Events;

namespace Haris.Core.Modules
{
	public interface IHarisModule: IDisposable
	{
		void Init();
	}

	public abstract class HarisModuleBase<TPayload> : IHarisModule, IHandle<TPayload> where TPayload: BaseEvent
	{
		public abstract void Dispose();
		public abstract void Init();
		public abstract void Handle(TPayload message);
	}
}