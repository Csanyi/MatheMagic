using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPanelScript : MonoBehaviour
{
    public TravelGameLogic GameLogic;
    public GameObject DispText;
    public void NumberIn(GameObject numButton)
    {
        Debug.Log(numButton.name);
        if (DispText.GetComponent<TextMeshProUGUI>().text.Length < 5)
        {
            DispText.GetComponent<TextMeshProUGUI>().text += numButton.GetComponentInChildren<TextMeshProUGUI>().text;
        }
    }

    public void DeleteNumber()
    {
        string TMP = DispText.GetComponent<TextMeshProUGUI>().text;
        if (TMP != "")
        {
            DispText.GetComponent<TextMeshProUGUI>().text = TMP.Remove(TMP.Length - 1, 1);
        }
    }

    public void CheckNumber()
    {
        string TMP = DispText.GetComponent<TextMeshProUGUI>().text;
        if (TMP !=  "")
        {
            int number = System.Int32.Parse(DispText.GetComponent<TextMeshProUGUI>().text);
            GameLogic.NumberEntered(number);
            DispText.GetComponent<TextMeshProUGUI>().text = "";
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] InputButtons = GameObject.FindGameObjectsWithTag("NumberButton");
        for (int i = 0; i<InputButtons.Length; i++)
        {
            Debug.Log(InputButtons[i].name);
            GameObject TMP = InputButtons[i];
            InputButtons[i].GetComponent<Button>().onClick.AddListener(delegate { NumberIn(TMP); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
