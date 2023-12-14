using Assets.Scripts.Persistence;
using System;
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
    public GameObject DispExercise;

    public bool boatIsMoving = false;
    public Travel travelLevel;
    //ezt még valahonnan meg kell kapnom (és a hosszt is)
    private Grade grade = Grade.FIRST;

    public void NumberIn(GameObject numButton)
    {
        Debug.Log(numButton.name);
        //Mi legyen a beviteli érték felso korlat?
        if ((DispText.GetComponent<TextMeshProUGUI>().text.Length < 5)&& !boatIsMoving)
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
            DispText.GetComponent<TextMeshProUGUI>().text = "";
            if (travelLevel.InputResult(number))
            {
                boatIsMoving = true;
            }
        }
        
    }
    // Start is called before the first frame update
    async void Start()
    {
        //Beégetett úthossz!
        var Db = new Database();
        await Db.CreateUserAsync(new User {Name = "Piroska", Class = 1, Character = Characters.Female});
        User piros = await Db.GetUserAsync();
        travelLevel = new Travel(4, grade);
        GameObject[] InputButtons = GameObject.FindGameObjectsWithTag("NumberButton");
        for (int i = 0; i<InputButtons.Length; i++)
        {
            Debug.Log(InputButtons[i].name);
            GameObject temp = InputButtons[i];
            InputButtons[i].GetComponent<Button>().onClick.AddListener(delegate { NumberIn(temp); });
        }
        DispExercise.GetComponent<TextMeshProUGUI>().text = travelLevel.GetCurrentExercise().ExerciseStringWithoutResult();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
