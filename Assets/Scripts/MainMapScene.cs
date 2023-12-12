using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMapScene : MonoBehaviour
{
	[SerializeField] private Button operationMapButton;
	[SerializeField] private Button profileButton;
	[SerializeField] private Canvas mapCanvas;

	private void Start()
	{
		mapCanvas.sortingOrder -= 1;
		operationMapButton.onClick.AddListener(() => SceneManager.LoadScene(2));
		profileButton.onClick.AddListener(() => SceneManager.LoadScene(1));
	}
}
