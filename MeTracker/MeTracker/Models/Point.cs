using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MeTracker.Models
{
	public class Point
	{
		public Location Location { get; set; }
		public int Count { get; set; } = 1;
		public Color Heat { get; set; }
	}
}
