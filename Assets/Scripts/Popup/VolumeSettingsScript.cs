using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsScript : MonoBehaviour
{
	[SerializeField] private Slider volumeSlider;

	private void Awake()
    {
		if (!PlayerPrefs.HasKey("volume"))
		{
			PlayerPrefs.SetFloat("volume", 1);
		}

		volumeSlider.onValueChanged.AddListener((value) => ChangeVolume(value));
		LoadVolume();
	}

	private void ChangeVolume(float value)
	{
		AudioListener.volume = volumeSlider.value;
		SaveVolume(value);
	}

	private void SaveVolume(float value)
	{
		PlayerPrefs.SetFloat("volume", value);
	}

	private void LoadVolume()
	{
		volumeSlider.value = PlayerPrefs.GetFloat("volume");
	}
}
