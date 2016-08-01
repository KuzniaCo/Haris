using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace Haris.DataModel.IntentRecognition
{
	public class CubeConfigDto
	{
		public Guid CubeId { get; set; }
		public string CubeLabel { get; set; }
		public HashSet<IntentLabel> SupportedIntents { get; set; }
		public IList<PowerIntentDto> TurOnIntentActions { get; set; }
		public IList<PowerIntentDto> TurnOffIntentActions { get; set; }
		public IList<PropertyRelatedIntentDto> GetIntentActions { get; set; }
		public IList<PropertyRelatedIntentDto> SetIntentActions { get; set; }

		[JsonIgnore, Obsolete]
		public EntityConfigDto[] Entities { get; set; }
	}
}