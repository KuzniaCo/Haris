using System.Collections.Generic;

namespace Haris.DataModel.Luis
{
	public class LuisParameter
	{
		public string Name { get; set; }
		public bool Required { get; set; }
		public List<LuisParameterValue> Value { get; set; }
	}
}