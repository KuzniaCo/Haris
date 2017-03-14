using System;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events;
using Haris.Core.Services.Logging;

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
		public bool IsBusy { get; set; }

		protected HarisModuleBase()
		{
			BusyEvent = new ManualResetEvent(false);
		}

		public ManualResetEvent BusyEvent { get; private set; }

		protected async void RunInBusyContextWithErrorFeedback<T>(Func<Task<T>> action, Action<T> callback = null)
		{
			SetBusy();
			var ret = await WrapAsync(action, default(T));
			if (callback != null && ret != null)
				callback(ret);
			ResetBusy();
		}

		protected async void RunInBusyContextWithErrorFeedback(Func<Task> action)
		{
			SetBusy();
			await WrapAsync(action);
			ResetBusy();
		}

		protected async void RunInBusyContextWithErrorFeedback<T>(Func<T> action, Action<T> callback = null)
		{
			SetBusy();
			var ret = await Wrap(action, default(T));
			if (callback != null && ret != null)
				callback(ret);
			ResetBusy();
		}

		protected async void RunInBusyContextWithErrorFeedback(Action action, Action<bool> callback = null,
			CancellationToken ct = new CancellationToken())
		{
			SetBusy();
			var ret = await Wrap(action, ct);
			if (callback != null)
				callback(ret);
			ResetBusy();
		}

		protected void RunInBusyContextWithErrorFeedback(Action action, CancellationToken ct = new CancellationToken())
		{
			RunInBusyContextWithErrorFeedback(action, null, ct);
		}

		protected async void GetAsyncWithReturn<T>(Func<Task<T>> action, Action<T> callback = null)
		{
			var ret = await WrapAsync(action, default(T));
			if (callback != null)
				callback(ret);
		}

		private void SetBusy()
		{
			BusyEvent.Reset();
			IsBusy = true;
		}

		private void ResetBusy()
		{
			IsBusy = false;
			BusyEvent.Set();
		}

		protected async Task<T> WrapAsync<T>(Func<Task<T>> action, T defaultValue)
		{
			T ret;
			try
			{
				ret = await action();
			}
			catch (Exception e)
			{
				HandleException(e);
				ret = defaultValue;
			}
			return ret;
		}

		protected async Task WrapAsync(Func<Task> action)
		{
			try
			{
				await action();
			}
			catch (Exception e)
			{
				HandleException(e);
			}
		}

		protected async Task<T> Wrap<T>(Func<T> action, T defaultValue, CancellationToken ct = new CancellationToken())
		{
			T ret;
			try
			{
				ret = await Task.Run(action, ct);
			}
			catch (Exception e)
			{
				HandleException(e);
				ret = defaultValue;
			}
			return ret;
		}

		private void HandleException(Exception e)
		{
			ShowFailureMessage(e.Message + " " + e.StackTrace);
		}

		protected async Task<bool> Wrap(Action action, CancellationToken ct = new CancellationToken())
		{
			var ret = true;
			try
			{

				await Task.Run(action, ct);
			}
			catch (Exception e)
			{
				HandleException(e);
				ret = false;
			}
			return ret;
		}

		protected async Task<bool> GetAsync(Action action)
		{
			var ret = true;
			BusyEvent.Reset();
			try
			{

				await Task.Run(action);
			}
			catch (Exception e)
			{
				HandleException(e);
				ret = false;
			}
			BusyEvent.Set();
			return ret;
		}

		protected virtual void ShowFailureMessage(string message = "")
		{
#if !DEBUG
			message = "";
#endif
			Logger.LogError("Something went wrong. Check your internet connection and try again." + message);
		}
	}
}