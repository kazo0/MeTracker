using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MeTracker.Models;

namespace MeTracker.Repositories
{
	public interface ILocationRepository
	{
		Task Save(Location location);
	}
}
