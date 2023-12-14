using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperationMapScene : MapSceneBase
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button sampleGameButton;
    [SerializeField] private Button lockGameButton;

    protected override void Awake()
    {
        base.Awake();
		homeButton.onClick.AddListener(() => SceneManager.LoadScene(0));
		sampleGameButton.onClick.AddListener(() => SceneManager.LoadScene(3));
		lockGameButton.onClick.AddListener(() => SceneManager.LoadScene(4));
	}
}
