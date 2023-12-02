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
        // Kezdetben elrejtj�k a pop-up ablakot

        if (popupPanel == null)
        {
            popupPanel = GameObject.Find("Settings");

            textObject = GameObject.Find("Hanyadik_evfolyam").GetComponent<TextMeshProUGUI>();

            // GameObject sliderObject = GameObject.Find("Slider");
            //VolumeSliderGet = GameObject.Find("Volume Slider").GetComponent<Slider>();


            // Ellen�rizz�k, hogy a GameObject megtal�lhat�-e
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
        // Megjelen�tj�k a pop-up ablakot
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        // Elrejtj�k a pop-up ablakot
        popupPanel.SetActive(false);
    }

    public void SelectRight()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0,1));
        if(currentYear < 4)
        {
            currentYear++;
        }
        textObject.text = currentYear + ". �vfolyam";
    }
    public void SelectLeft()
    {
        byte currentYear = Byte.Parse(textObject.text.Substring(0, 1));
        if (currentYear > 1)
        {
            currentYear--;
        }
        textObject.text = currentYear + ". �vfolyam";

    }

    private void OnSliderValueChanged(float value)
    {
        // Mentse el az aktu�lis �rt�ket a v�ltoz�ba
        float savedValue = value;
        Debug.Log(savedValue);

        // Ide �rhatod az �rt�k ment�s�vel kapcsolatos tov�bbi logik�t vagy f�jlba �r�st
    }

    public void ClickedSave()
    {
        string pickedYear = textObject.text;
        //float volume = volumeSlider.value;

        Debug.Log(pickedYear);

        //TODO: Slider �rt�ket lek�rni 
        //TODO �vfolyamot adatb�zisba elk�ldeni + a Slider �rt�knek megfelel� hanger�t be�ll�tani

    }
}
