using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject PausePopup;

    private void Awake()
    {
        homeButton.onClick.AddListener(() => SceneManager.LoadScene(2));
		resumeButton.onClick.AddListener(() => PausePopup.SetActive(false));
		pauseButton.onClick.AddListener(() => PausePopup.SetActive(true));
	}
}
