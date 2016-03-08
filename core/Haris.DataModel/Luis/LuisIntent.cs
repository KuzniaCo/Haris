﻿using System;
using Haris.DataModel.IntentRecognition;

namespace Haris.DataModel.Luis
{
	public class LuisIntent
	{
		public string Intent { get; set; }
		public double Score { get; set; }

		public IntentLabel IntentLabel
		{
			get
			{
				IntentLabel label;
				var result = Enum.TryParse(Intent, out label);
				return result ? label : IntentLabel.None;
			}
		}
	}
}