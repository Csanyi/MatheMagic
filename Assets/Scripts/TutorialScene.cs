using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScene : MonoBehaviour
{
    [SerializeField] private Button nextButton;
	[SerializeField] private GameObject canvas;

	private async void Awake()
	{
		await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		});

		var auth = FirebaseAuth.DefaultInstance;

		if (auth.CurrentUser is not null)
		{
			SceneManager.LoadScene(2);
		} 
		else
		{
			canvas.SetActive(true);
			nextButton.onClick.AddListener(() => SceneManager.LoadScene(1));
		}
	}
}
