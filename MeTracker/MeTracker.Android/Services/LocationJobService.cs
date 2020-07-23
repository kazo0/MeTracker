using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MeTracker.Repositories;

namespace MeTracker.Droid.Services
{
	[Service(Name = "MeTracker.Droid.Services.LocationJobService",
		Permission = "android.permission.BIND_JOB_SERVICE")]
	public class LocationJobService : JobService, ILocationListener
	{
		private readonly ILocationRepository _locationRepository;
		private static LocationManager _locationManager;

		public LocationJobService(ILocationRepository locationRepository)
		{
			_locationRepository = locationRepository;
		}

		public override bool OnStartJob(JobParameters @params)
		{
			_locationManager = (LocationManager) Application.Context.GetSystemService(Context.LocationService);
			_locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 1000L, 0.1f, this);

			return true;
		}

		public override bool OnStopJob(JobParameters @params)
		{
			return true;
		}

		public void OnLocationChanged(Location location)
		{
			var locationModel = new Models.Location(location.Latitude, location.Longitude);
			_locationRepository.Save(locationModel);
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, Availability status, Bundle extras)
		{
		}
	}
}