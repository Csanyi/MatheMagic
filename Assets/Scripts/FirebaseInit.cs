using Firebase;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseInit : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private InputField nameField;
	[SerializeField] private InputField classField;

    void Start()
    {
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Debug.Log("Firebase init");
		});
    }
}
