using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MeTracker.Controls;
using MeTracker.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MeTracker.Droid.Renderers
{
	public class CustomMapRenderer : MapRenderer
	{
		public CustomMapRenderer(Context context) : base(context)
		{
			
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == CustomMap.PointsProperty.PropertyName)
			{
				if (NativeMap == null)
				{
					return;
				}

				var customMap = (CustomMap) Element;
				foreach (var point in customMap.Points)
				{
					var options = new CircleOptions();
					options.InvokeStrokeWidth(0)
						.InvokeFillColor(point.Heat.ToAndroid())
						.InvokeRadius(200d)
						.InvokeCenter(new LatLng(point.Location.Latitude, point.Location.Longitude));
					
					NativeMap.AddCircle(options);
				}
			}
		}
	}
}