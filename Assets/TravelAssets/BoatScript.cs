using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public InputPanelScript panelScript;
    public float movementSpeed = 2f;
    public GameObject BoatPath;
    private Transform[] ControlPoints;
    //beegetett ertek
    private int cpNum = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Egyelore fix 4 kontrolponttal dolgozom, ezt meg lehet fordítani és az uthosszbol legenerálni a kontrollpontokat
        ControlPoints = new Transform[cpNum];
        for (int i = 0; i<cpNum; i++)
        {
            ControlPoints[i] = BoatPath.transform.GetChild(i);
        }
        for (int i = 0; i<ControlPoints.Length; ++i)
        {
            Debug.Log(ControlPoints[i].name);
        }
        transform.position = ControlPoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (panelScript.boatIsMoving)
        {
            Vector3 goalPosition = ControlPoints[panelScript.travelLevel.GetCurrentPositionOnPath()].position;
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, movementSpeed * Time.deltaTime);
            //a befejezés még nincs megoldva
            if (Vector3.Distance(transform.position, goalPosition) <=0.01 && !panelScript.travelLevel.IsFinished())
            {
                panelScript.boatIsMoving = false;
                panelScript.DispExercise.GetComponent<TextMeshProUGUI>().text = panelScript.travelLevel.GetCurrentExercise().ExerciseStringWithoutResult();
            }
        }   
    }
}
