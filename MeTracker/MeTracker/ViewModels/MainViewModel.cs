using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeTracker.Repositories;
using MeTracker.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;
using Point = MeTracker.Models.Point;

namespace MeTracker.ViewModels
{
	public class MainViewModel : ViewModel
	{
		private readonly ILocationRepository _locationRepository;
		private readonly ILocationTrackingService _locationTrackingService;

		public List<Point> Points { get; set; }

		public MainViewModel(
			ILocationRepository locationRepository,
			ILocationTrackingService locationTrackingService)
		{
			_locationRepository = locationRepository;
			_locationTrackingService = locationTrackingService;

			MainThread.BeginInvokeOnMainThread(async () =>
			{
				_locationTrackingService.StartTracking();
				await LoadData();
			});
		}

		private async Task LoadData()
		{
			var locations = await _locationRepository.GetAll();
			var points = new List<Point>();

			foreach (var location in locations)
			{
				if (points.None())
				{
					points.Add(new Point
					{
						Location = location,
					});
					continue;
				}
				var pointFound = false;
				//try to find a point for the current location
				foreach (var point in points)
				{
					var distance =
						Xamarin.Essentials.Location.CalculateDistance(
							new Xamarin.Essentials.Location(
								point.Location.Latitude, point.Location.Longitude),
							new Xamarin.Essentials.Location(location.Latitude,
								location.Longitude), DistanceUnits.Kilometers);
					if (distance < 0.2)
					{
						pointFound = true;
						point.Count++;
						break;
					}
				}

				//if no point is found, add a new Point to the list of points
				if (!pointFound)
				{
					points.Add(new Models.Point() { Location = location });
				}
			}

			if (points.None())
			{
				return;
			}

			points = points.OrderBy(x => x.Count).ToList();
			
			var pointsMax = points.Last().Count;
			var pointsMin = points.First().Count;
			var diff = (float) (pointsMax - pointsMin);

			foreach (var point in points)
			{
				var heat = (2f / 3f) - ((float)point.Count / diff);
				point.Heat = Color.FromHsla(heat, 1, 0.5);
			}

			Points = points;
		}
	}
}
