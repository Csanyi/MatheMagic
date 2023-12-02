using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{   
    public GameObject popupPanel;
    public TextMeshProUGUI textObject;
    public Slider VolumeSliderGet;

    private void Start()
    {
        // Kezdetben elrejtjük a pop-up ablakot

        if (popupPanel == null)
        {
            popupPanel = GameObject.Find("Settings");

            textObject = GameObject.Find("Hanyadik_evfolyam").GetComponent<TextMeshProUGUI>();

            // GameObject sliderObject = GameObject.Find("Slider");
            //VolumeSliderGet = GameObject.Find("Volume Slider").GetComponent<Slider>();


            // Ellenõrizzük, hogy a GameObject megtalálható-e
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
        // Megjelenítjük a pop-up ablakot
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        // Elrejtjük a pop-up ablakot
        popupPanel.SetActive(false);
    }

    public void SelectRight()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0,1));
        if(currentYear < 4)
        {
            currentYear++;
        }
        textObject.text = currentYear + ". Évfolyam";
    }
    public void SelectLeft()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0, 1));
        if (currentYear > 1)
        {
            currentYear--;
        }
        textObject.text = currentYear + ". Évfolyam";

    }

    private void OnSliderValueChanged(float value)
    {
        // Mentse el az aktuális értéket a változóba
        float savedValue = value;
        Debug.Log(savedValue);

        // Ide írhatod az érték mentésével kapcsolatos további logikát vagy fájlba írást
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
