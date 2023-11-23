using System.Collections.Generic;
using Firebase.Firestore;

namespace Assets.Scripts.Persistence
{
	public enum Subtopics 
	{
		Addition,
		Subtraction,
		Multiplication,
		Division,
	}

	[FirestoreData]
	public class Path
	{
		[FirestoreDocumentId]
		public string Id { get; set; }

		[FirestoreProperty]
		public Subtopics Subtopic { get; set; }

		[FirestoreProperty]
		public List<Level> Levels { get; set; }
	}
}
