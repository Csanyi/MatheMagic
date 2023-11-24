using Firebase.Firestore;
using System;

namespace Assets.Scripts.Persistence
{
	public enum Characters
	{
		Male,
		Female,
	};

	[FirestoreData]
	public class User
	{
		private string _name = "DefaultName";
		private int _class = 1;

		[FirestoreDocumentId]
		public string Id { get; set; }

		[FirestoreProperty]
		public string Name 
		{
			get => _name;
			set
			{
				if (string.IsNullOrEmpty(_name))
				{
					throw new ArgumentException("Name cannot be empty.");
				}

				_name = value;
			}
		}

		[FirestoreProperty]
		public int Class
		{
			get => _class;
			set
			{
				if (value < 1 || value > 4)
				{
					throw new ArgumentException("Class need to be between 1 to 4.");
				}

				_class = value;
			}
		}

		[FirestoreProperty]
		public int Xp { get; set; }

		[FirestoreProperty]
		public Characters Character {  get; set; }
	}
}
