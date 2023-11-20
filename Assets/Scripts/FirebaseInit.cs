using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;
using UnityEngine;
using UnityEngine.UI;

[FirestoreData]
public struct TestData
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public int Class { get; set; }
}

public class FirebaseInit : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InputField nameField;
	[SerializeField] private InputField classField;

	private const string path = "users/user1";

    void Start()
    {
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Debug.Log("Firebase init");
		});

        button.onClick.AddListener(() =>
        {
			var data = new TestData
			{
				Name = nameField.text,
				Class = int.Parse(classField.text),
			};

			var fireStore = FirebaseFirestore.DefaultInstance;
			fireStore.Document(path).SetAsync(data);
            Debug.Log($"Data sent to db: {data.Name} {data.Class}");
		});
    }
}
