﻿using System;
using System.Collections.Generic;

namespace P04WeatherForecastAPI.Client.Models
{
	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class DailyForecast
	{
		public DateTime Date { get; set; }
		public int EpochDate { get; set; }
		public TemperatureRange Temperature { get; set; }
		public List<string> Sources { get; set; }
		public string MobileLink { get; set; }
		public string Link { get; set; }
	}

	public class Day
	{
		public int Icon { get; set; }
		public string IconPhrase { get; set; }
		public bool HasPrecipitation { get; set; }
	}

	public class Headline
	{
		public DateTime EffectiveDate { get; set; }
		public int EffectiveEpochDate { get; set; }
		public int Severity { get; set; }
		public string Text { get; set; }
		public string Category { get; set; }
		public DateTime EndDate { get; set; }
		public int EndEpochDate { get; set; }
		public string MobileLink { get; set; }
		public string Link { get; set; }
	}

	public class Maximum
	{
		public int Value { get; set; }
		public string Unit { get; set; }
		public int UnitType { get; set; }
	}

	public class Minimum
	{
		public int Value { get; set; }
		public string Unit { get; set; }
		public int UnitType { get; set; }
	}

	public class Night
	{
		public int Icon { get; set; }
		public string IconPhrase { get; set; }
		public bool HasPrecipitation { get; set; }
	}

	public class Root
	{
		public Headline Headline { get; set; }
		public List<DailyForecast> DailyForecasts { get; set; }
	}
}
