using System.Collections.Generic;

namespace Haris.DataModel.Luis
{
	public class LuisAction
	{
		public bool Triggered { get; set; }
		public string Name { get; set; }
		public List<LuisParameter> Parameters { get; set; }
	}
}