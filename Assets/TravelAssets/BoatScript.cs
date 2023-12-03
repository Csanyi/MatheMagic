using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public InputPanelScript panelScript;
    public float movementSpeed = 2f;
    public GameObject BoatPath;
    private Transform[] ControlPoints;
    private int cpNum;

    // Start is called before the first frame update
    void Start()
    {
        cpNum = BoatPath.transform.childCount;
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
        if (panelScript.gameState == 1)
        {
            int pointInd = 0;
            if (panelScript.currentPoint < cpNum)
            {
                pointInd = panelScript.currentPoint;
            }
            Vector3 goalPosition = ControlPoints[pointInd].position;
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, goalPosition) <=0)
            {
                panelScript.gameState = 0;
            }
        }   
    }
}
