using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperationMapScene : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button sampleGameButton;
    [SerializeField] private Button lockGameButton;
	[SerializeField] private Canvas mapCanvas;

    private void Start()
    {
		mapCanvas.sortingOrder -= 1;
		homeButton.onClick.AddListener(() => SceneManager.LoadScene(0));
		sampleGameButton.onClick.AddListener(() => SceneManager.LoadScene(3));
		lockGameButton.onClick.AddListener(() => SceneManager.LoadScene(4));
	}
}
