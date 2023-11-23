using Firebase.Firestore;

namespace Assets.Scripts.Persistence
{
	public enum LevelTypes
	{
		Coloring,
		Distribution,
		Lock,
		TileTrap,
		Traveling,
	};

	[FirestoreData]
	public class Level
	{
		[FirestoreDocumentId]
		public string Id { get; set; }

		[FirestoreProperty]
		public int Number { get; set; }

		[FirestoreProperty]
		public LevelTypes Type { get; set; }
	}
}
