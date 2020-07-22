using System;
using System.Collections.Generic;
using System.Text;
using MeTracker.Repositories;
using MeTracker.Services;
using Xamarin.Essentials;

namespace MeTracker.ViewModels
{
	public class MainViewModel : ViewModel
	{
		private readonly ILocationRepository _locationRepository;
		private readonly ILocationTrackingService _locationTrackingService;

		public MainViewModel(
			ILocationRepository locationRepository,
			ILocationTrackingService locationTrackingService)
		{
			_locationRepository = locationRepository;
			_locationTrackingService = locationTrackingService;

			MainThread.BeginInvokeOnMainThread(async () =>
			{
				_locationTrackingService.StartTracking();
			});
		}
	}
}
