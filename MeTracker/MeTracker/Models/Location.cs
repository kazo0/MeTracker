using System;
using System.Collections.Generic;
using System.Text;

namespace MeTracker.Models
{
	public class Location
	{
		public int Id { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }

		public Location(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

	}
}
