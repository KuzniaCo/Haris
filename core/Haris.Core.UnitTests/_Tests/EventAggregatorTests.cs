using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Haris.Core.Events;
using Haris.Core.Infrastructure;
using Haris.Core.Modules;
using NUnit.Framework;

namespace Haris.Core.UnitTests._Tests
{
	[TestFixture]
	public class EventAggregatorTests
	{
		#region helper classes
		private class TestEvent : BaseEvent<int>
		{
			public TestEvent(int payload) : base(payload)
			{
			}
		}

		private class TestEventHandler : HarisModuleBase<TestEvent>
		{
			private readonly ConcurrentBag<int> _targetBag;

			public TestEventHandler(ConcurrentBag<int> targetBag)
			{
				_targetBag = targetBag;
			}

			public override void Dispose()
			{
				// aggregator subscription managed externally
			}

			public override void Init()
			{
				// aggregator subscription managed externally
			}

			public override void Handle(TestEvent message)
			{
				_targetBag.Add(message.Payload);
			}
		}
		#endregion

		[TestCase(1, 1)]
		[TestCase(5, 1)]
		[TestCase(5, 100)]
		[TestCase(5, 1000)]
		[TestCase(3, 3000)]
		public void PassesAllEventsForward(int numBunches, int itemsPerBunch)
		{
			var sut = new EventAggregator { PublicationThreadMarshaller = QueueAsync };

			var bag = new ConcurrentBag<int>();
			var parallelPublishAction = new Action<IEnumerable<TestEvent>>(ecents => Parallel.ForEach(ecents, e => sut.Publish(e)));
			var bunches =
				Enumerable.Range(0, numBunches)
					.Select(
						i =>
							new Action(
								() =>
									parallelPublishAction(Enumerable.Range(itemsPerBunch*i, itemsPerBunch).Select(j => new TestEvent(j)).ToArray())));


			var handler = new TestEventHandler(bag);
			sut.Subscribe(handler);
			Parallel.Invoke(bunches.ToArray());
			AsyncActionsQueue._waitForDrying();
			sut.Unsubscribe(handler);

			for (var i = 0; i < itemsPerBunch * numBunches; i++)
			{
				Assert.True(bag.Contains(i), $"element {i} not found");
			}
		}

		private void QueueAsync(Action action) //copied from Core
		{
			AsyncActionsQueue.Enqueue(action);
		}
	}
}