using System;
using Haris.DataModel.Action;
// ReSharper disable InconsistentNaming

namespace Haris.DataModel.IntentRecognition
{
	public class CubeConfigDto
	{
		public Guid CubeId { get; set; }
		public EntityConfigDto[] Entities { get; set; }
	}

	public class EntityConfigDto
	{
		public string[] EntityTags { get; set; }
		public IntentConfigDto[] IntentConfigurations { get; set; }
	}

	public class IntentConfigDto
	{
		public IntentLabel IntentLabel { get; set; }
		public ActionDescriptorDto[] Actions { get; set; }
	}

	public enum IntentLabel
	{
		None,
		TurnOn,
		TurnOff,
		Set,
		Get
	} 
}