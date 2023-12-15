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
    private bool FinishIsCalled;
    //beegetett ertek
    private int cpNum = 8;

    [SerializeField] private GameObject clearLevelPopup;

    // Start is called before the first frame update
    void Start()
    {
        FinishIsCalled = false;
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
    async void Update()
    {   
        if (panelScript.boatIsMoving)
        {
            Vector3 goalPosition = ControlPoints[panelScript.travelLevel.GetCurrentPositionOnPath()].position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position-goalPosition);
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, movementSpeed * Time.deltaTime);
            //a befejezés még nincs megoldva
            bool arrived = Vector3.Distance(transform.position, goalPosition) <= 0.01;
            if (arrived && !panelScript.travelLevel.IsFinished())
            {
                panelScript.boatIsMoving = false;
                panelScript.DispExercise.GetComponent<TextMeshProUGUI>().text = panelScript.travelLevel.GetCurrentExercise().ExerciseStringWithoutResult();
            }
            else if (arrived && panelScript.travelLevel.IsFinished() && !FinishIsCalled)
            {
                FinishIsCalled = true;
                ClearLevelScript script = clearLevelPopup.GetComponentInChildren<ClearLevelScript>();
                script.SceneToLoad = 8;
                clearLevelPopup.SetActive(true);
                await script.TaskCompleted();
            }
        }
    }
}
