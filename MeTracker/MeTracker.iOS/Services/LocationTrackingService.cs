using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MeTracker.Models;
using MeTracker.Repositories;
using MeTracker.Services;
using UIKit;

namespace MeTracker.iOS.Services
{
	public class LocationTrackingService : ILocationTrackingService
	{
		private readonly ILocationRepository _locationRepository;
		private CLLocationManager _locationManager;

		public LocationTrackingService(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;
		}

		public void StartTracking()
		{
			_locationManager = new CLLocationManager
			{
				PausesLocationUpdatesAutomatically = false,
				AllowsBackgroundLocationUpdates = true,
			};

			_locationManager.AuthorizationChanged += LocationAuthChanged;
			_locationManager.RequestAlwaysAuthorization();
		}

		private void LocationAuthChanged(object sender, CLAuthorizationChangedEventArgs e)
		{
			if (e.Status == CLAuthorizationStatus.Authorized)
			{
				_locationManager.DesiredAccuracy = CLLocation.AccurracyBestForNavigation;

				_locationManager.LocationsUpdated += async (o, args) =>
				{
					var latestLocation = args.Locations.LastOrDefault();
					if (latestLocation == null)
					{
						return;
					}

					var newLocation = new Location(latestLocation.Coordinate.Latitude, latestLocation.Coordinate.Longitude);
					await _locationRepository.Save(newLocation);
				};
				_locationManager.StartUpdatingLocation();
			}
		}
	}
}
			