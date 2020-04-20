using System;
using Newtonsoft.Json;

namespace alpairdotcomextractor.models
{
	public class RatesAll
	{
		[JsonProperty("data")]
		public double[][] Data {get; set;}
	}
}
