using Firebase.Auth;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence
{
	public class Database
	{
		private readonly FirebaseFirestore _db;
		private readonly FirebaseAuth _auth;

		public Database() 
		{
			_db = FirebaseFirestore.DefaultInstance;
			_auth = FirebaseAuth.DefaultInstance;
		}

		public string GetUserId()
		{
			if (_auth.CurrentUser is null)
			{
				return null;
			}

			return _auth.CurrentUser.UserId;
		}

		public async Task CreateUserAsync(User user)
		{
			if(_auth.CurrentUser is null)
			{
				var result = await _auth.SignInAnonymouslyAsync();

				if (result is not null)
				{
					await _db.Collection("users").Document(_auth.CurrentUser.UserId).SetAsync(user);
				}
			}
		}

		public async Task UpdateUserAsync(User user)
		{
			if (_auth.CurrentUser is null)
			{
				return;
			}

			await _db.Collection("users").Document(_auth.CurrentUser.UserId).SetAsync(user);
		}

		public async Task<User> GetUserAsync()
		{
			if (_auth.CurrentUser is null)
			{
				return null;
			}

			return (await _db.Collection("users").Document(_auth.CurrentUser.UserId).GetSnapshotAsync()).ConvertTo<User>();
		}

		public async Task<IList<Level>> GetUserProgressAsync()
		{
			if (_auth.CurrentUser is null)
			{
				return null;
			}

			return (await _db.Collection($"users/{_auth.CurrentUser.UserId}/progress").GetSnapshotAsync()).Documents.Select(d => d.ConvertTo<Level>()).ToList();
		}

		public async Task UpdateProgressAsync(string pathId, Level level)
		{
			if (_auth.CurrentUser is null)
			{
				return;
			}

			await _db.Collection("users").Document(_auth.CurrentUser.UserId).Collection("progress").Document(pathId).SetAsync(level);
		}

		public async Task<IList<Map>> GetMapsAsync()
		{
			return (await _db.Collection("maps").GetSnapshotAsync()).Documents.Select(d => d.ConvertTo<Map>()).ToList();
		}

		public async Task<IList<Map>> GetClassMapsAsync(int classNumber)
		{
			return (await _db.Collection("maps").WhereEqualTo(nameof(Map.Class), classNumber).GetSnapshotAsync()).Documents.Select(d => d.ConvertTo<Map>()).ToList();
		}

		public async Task<Map> GetMapAsync(string mapId)
		{
			return (await _db.Document($"maps/{mapId}").GetSnapshotAsync()).ConvertTo<Map>();
		}

		public async Task<IList<Path>> GetPathsAsync(string mapId)
		{
			return (await _db.Collection($"maps/{mapId}/paths").GetSnapshotAsync()).Documents.Select(d => d.ConvertTo<Path>()).ToList();
		}

		public async Task<Path> GetPathAsync(string mapId, string pathId)
		{
			return (await _db.Document($"maps/{mapId}/paths/{pathId}").GetSnapshotAsync()).ConvertTo<Path>();
		}
	}
}
