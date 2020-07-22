using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Foundation;
using MeTracker.iOS.Services;
using MeTracker.Services;
using UIKit;

namespace MeTracker.iOS
{
	public class Bootstrapper : MeTracker.Bootstrapper
	{
		public static void Init()
		{
			_ = new Bootstrapper();
		}

		protected override void PlatformInitialize()
		{
			ContainerBuilder.RegisterType<LocationTrackingService>()
				.As<ILocationTrackingService>()
				.SingleInstance();
		}
	}
}