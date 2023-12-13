using Assets.Scripts.Persistence;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMapScene : MonoBehaviour
{
	[SerializeField] private Button operationMapButton;
	[SerializeField] private Button profileButton;
	[SerializeField] private Canvas mapCanvas;

	private async void Start()
	{
		await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(async task =>
		{
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			var db = new Database();
			if (db.GetUserId() is null)
			{
				await db.CreateUserAsync(new User { Name = "Brendon", Class = 1, Character = Characters.Male, Xp = 0 });
			}
			Debug.Log("Firebase init");
		});
		

		mapCanvas.sortingOrder -= 1;
		operationMapButton.onClick.AddListener(() => SceneManager.LoadScene(2));
		profileButton.onClick.AddListener(() => SceneManager.LoadScene(1));
	}
}
