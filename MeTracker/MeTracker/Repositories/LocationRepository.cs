using System;
using System.IO;
using System.Threading.Tasks;
using MeTracker.Models;
using SQLite;

namespace MeTracker.Repositories
{
	public class LocationRepository : ILocationRepository
	{
		private SQLiteAsyncConnection _connection;
		
		public async Task Save(Location location)
		{
			await CreateConnection();

			await _connection.InsertAsync(location);
		}

		private async Task CreateConnection()
		{
			if (_connection != null)
			{
				return;
			}

			var databasePath =
				Path.Combine(Environment.GetFolderPath
					(Environment.SpecialFolder.MyDocuments), "Locations.db");
			_connection = new SQLiteAsyncConnection(databasePath);

			await _connection.CreateTableAsync<Location>();
		}
	}
}
