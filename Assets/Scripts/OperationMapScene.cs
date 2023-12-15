using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperationMapScene : MapSceneBase
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button sampleGameButton;
    [SerializeField] private Button colorGameButton;
    [SerializeField] private Button lockGameButton;
    [SerializeField] private Button tileGameButton;
    [SerializeField] private Button travelGameButton;

    protected override void Awake()
    {
        base.Awake();
		homeButton.onClick.AddListener(() => SceneManager.LoadScene(2));
		//sampleGameButton.onClick.AddListener(() => SceneManager.LoadScene(5));
        colorGameButton.onClick.AddListener(() => SceneManager.LoadScene(5));
		lockGameButton.onClick.AddListener(() => SceneManager.LoadScene(6));
        tileGameButton.onClick.AddListener(() => SceneManager.LoadScene(7));
        travelGameButton.onClick.AddListener(() => SceneManager.LoadScene(8));
    }
}
