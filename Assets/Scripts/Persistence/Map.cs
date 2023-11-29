using Firebase.Firestore;
using System;

namespace Assets.Scripts.Persistence
{
	public enum Topics 
	{ 
		Operations,
	};

	[FirestoreData]
	public class Map
	{
		private int _class = 1;

		[FirestoreDocumentId]
		public string Id { get; set; }

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
		public Topics Topic { get; set; }
	}
}
