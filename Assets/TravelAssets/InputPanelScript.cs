using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InputPanelScript : MonoBehaviour
{
    public TravelGameLogic GameLogic;
    public GameObject DispText;

    public int gameState = 0;
    public int currentPoint = 0;

    public void NumberIn(GameObject numButton)
    {
        Debug.Log(numButton.name);
        if ((DispText.GetComponent<TextMeshProUGUI>().text.Length < 5)&& gameState == 0)
        {
            DispText.GetComponent<TextMeshProUGUI>().text += numButton.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }

    public void DeleteNumber()
    {
        string temp = DispText.GetComponent<TextMeshProUGUI>().text;
        if (temp != "")
        {
            DispText.GetComponent<TextMeshProUGUI>().text = temp.Remove(temp.Length - 1, 1);
        }
    }

    public void CheckNumber()
    {
        string temp = DispText.GetComponent<TextMeshProUGUI>().text;
        if (temp !=  "")
        {
            int number = System.Int32.Parse(DispText.GetComponent<TextMeshProUGUI>().text);
            GameLogic.NumberEntered(number);
            DispText.GetComponent<TextMeshProUGUI>().text = "";
            if (true)
            {
                gameState = 1;
                currentPoint += 1;
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] InputButtons = GameObject.FindGameObjectsWithTag("NumberButton");
        for (int i = 0; i<InputButtons.Length; i++)
        {
            Debug.Log(InputButtons[i].name);
            GameObject temp = InputButtons[i];
            InputButtons[i].GetComponent<Button>().onClick.AddListener(delegate { NumberIn(temp); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
