using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGameLogicScript : MonoBehaviour
{
    public bool TileClicked(int x, int y)
    {
        return (x * y) % 2 == 0;
    }

    public int GetTileNumber(int x, int y)
    {
        return Random.Range(0, 9);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
