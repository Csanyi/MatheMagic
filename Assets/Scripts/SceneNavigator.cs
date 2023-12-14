using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    public GameObject quitCanvas;
    public Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Display the quit confirmation pop-up
            if (quitCanvas)
            {
                quitCanvas.SetActive(true);
            }
        }
    }

    public void SwitchToMainMap()
    {
        SceneManager.LoadScene(2);
    }

    public void SwitchToProfile()
    {
        SceneManager.LoadScene(3);
    }

    public void SwitchToOperationMap()
    {
        SceneManager.LoadScene(4);
    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene(6);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }
}
