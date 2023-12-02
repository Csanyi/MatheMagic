using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : MonoBehaviour
{   
    public GameObject popupPanel;
    public TextMeshProUGUI textObject;
    public Slider VolumeSliderGet;

    private void Start()
    {
        if (popupPanel == null)
        {
            popupPanel = GameObject.Find("Settings");

            textObject = GameObject.Find("Hanyadik_evfolyam").GetComponent<TextMeshProUGUI>();

            // GameObject sliderObject = GameObject.Find("Slider");
            //VolumeSliderGet = GameObject.Find("Volume Slider").GetComponent<Slider>();

            if (popupPanel == null)
            {
                Debug.LogError("PopupPanel not assigned and could not be found!");
            }
        }

        popupPanel.SetActive(false);
        //VolumeSliderGet.onValueChanged.AddListener(OnSliderValueChanged);



    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    public void SelectRight()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0,1));
        if(currentYear < 4)
        {
            currentYear++;
        }
        textObject.text = currentYear + ". Osztály";
    }
    public void SelectLeft()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0, 1));
        if (currentYear > 1)
        {
            currentYear--;
        }
        textObject.text = currentYear + ". Osztály";

    }

    private void OnSliderValueChanged(float value)
    {
        
        float savedValue = value;
        Debug.Log(savedValue);

        
    }

    public void ClickedSave()
    {
        string pickedYear = textObject.text;
        //float volume = volumeSlider.value;

        Debug.Log(pickedYear);

        //TODO: Slider értéket lekérni 
        //TODO Évfolyamot adatbázisba elküldeni + a Slider értéknek megfelelõ hangerõt beállítani

    }
}
