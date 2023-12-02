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
        gradeText.text = currentYear + ". Oszt�ly";
    }
    public void SelectLeft()
    {
        currentYear = Byte.Parse(gradeText.text.Substring(0, 1));
        if (currentYear > 1)
        {
            currentYear--;
        }
        gradeText.text = currentYear + ". Oszt�ly";

    }

    //TODO N�v hely�n az aktu�lis j�t�kos neve jelenjen meg
    //TODO Tov�bb l�p�s eset�n a kiv�lasztott oszt�lyt elmenteni az adatb�zisban

   
}
