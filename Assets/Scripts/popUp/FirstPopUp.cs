using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstPopUp : MonoBehaviour
{
    public GameObject firstPopUp;
    public TextMeshProUGUI gradeText;
    private byte currentYear;

    private void Start()
    {
        if (firstPopUp == null)
        {
            firstPopUp = GameObject.Find("FirstpopUp");

            gradeText = GameObject.Find("Grade_Text").GetComponent<TextMeshProUGUI>();

            if (firstPopUp == null)
            {
                Debug.LogError("PopupPanel not assigned and could not be found!");
            }
        }

        firstPopUp.SetActive(false);

    }

    public void ShowPopup()
    {
        firstPopUp.SetActive(true);
    }

    public void ClosePopup()
    {
        firstPopUp.SetActive(false);
    }

    public void SelectRight()
    {
        currentYear = Byte.Parse(gradeText.text.Substring(0, 1));
        if (currentYear < 4)
        {
            currentYear++;
        }
        gradeText.text = currentYear + ". Osztály";
    }
    public void SelectLeft()
    {
        currentYear = Byte.Parse(gradeText.text.Substring(0, 1));
        if (currentYear > 1)
        {
            currentYear--;
        }
        gradeText.text = currentYear + ". Osztály";

    }

    //TODO Név helyén az aktuális játékos neve jelenjen meg
    //TODO Tovább lépés esetén a kiválasztott osztályt elmenteni az adatbázisban

   
}
