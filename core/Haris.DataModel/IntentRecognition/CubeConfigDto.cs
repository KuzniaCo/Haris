using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace Haris.DataModel.IntentRecognition
{
	public class CubeConfigDto
	{
		public Guid CubeId { get; set; }
		public HashSet<IntentLabel> SupportedIntents { get; set; } 
		public EntityConfigDto[] Entities { get; set; }
	}
}