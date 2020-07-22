using Autofac;
using MeTracker.Droid.Services;
using MeTracker.Services;

namespace MeTracker.Droid
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