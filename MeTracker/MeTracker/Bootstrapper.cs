using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Core;
using MeTracker.Repositories;
using MeTracker.Services;
using MeTracker.ViewModels;
using Xamarin.Forms;

namespace MeTracker
{
	public abstract class Bootstrapper
	{
		protected ContainerBuilder ContainerBuilder { get; private set; }
		protected abstract void PlatformInitialize();

		protected Bootstrapper()
		{
			Initialize();
			FinishInitialization();
		}

		private void Initialize()
		{
			ContainerBuilder = new ContainerBuilder();

			var types = Assembly.GetExecutingAssembly()
				.DefinedTypes
				.Where(x => x.IsSubclassOf(typeof(ViewModel)) ||
				            x.IsSubclassOf(typeof(Page)));

			foreach (var type in types)
			{
				ContainerBuilder.RegisterType(type.AsType());
			}

			ContainerBuilder.RegisterType<LocationRepository>()
				.As<ILocationRepository>();

			PlatformInitialize();
		}

		private void FinishInitialization()
		{
			Resolver.Initialize(ContainerBuilder.Build());
		}
	}
}
