using Firebase;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseInit : MonoBehaviour
{
    [SerializeField] private Text text;

    void Start()
    {
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			text.text = "Yuhuu!";
		});
    }
}
