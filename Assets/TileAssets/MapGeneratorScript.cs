using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapGeneratorScript : MonoBehaviour
{

    public int rows;
    public int cols;

    public int spriteWidth;
    public int spriteHeight;

    public GameObject tilePrefab;
    public Canvas mainCanvas;
    public TileGameLogicScript gameLogic;

    public Sprite wrongAsnwerSprite;
    public Sprite correctAsnwerSprite;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = new Vector3(-50, -50, 0);

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                int x = i;
                int y = j;
                GameObject newTile = Instantiate(tilePrefab);
                newTile.transform.SetParent(mainCanvas.transform, false);
                newTile.GetComponent<RectTransform>().localPosition = new Vector3(j * spriteWidth, i * spriteHeight, 0) + offset;
                newTile.name = "Tile_" + x.ToString() + y.ToString();
                newTile.GetComponent<Button>().onClick.AddListener(delegate { tileOnClick(newTile, x, y); } );
                newTile.GetComponentInChildren<TextMeshProUGUI>().text = gameLogic.GetTileNumber(x, y).ToString();
            }
        }
    }

    public void tileOnClick(GameObject tile, int x, int y)
    {
        Debug.Log(tile.name + " at " + x.ToString() + ", " + y.ToString() + " was clicked.");
        bool result = gameLogic.TileClicked(x, y);
        if (result)
        {
            tile.GetComponent<Image>().sprite = correctAsnwerSprite;
        }
        else
        {
            tile.GetComponent<Image>().sprite = wrongAsnwerSprite;
        }
        tile.GetComponentInChildren<TextMeshProUGUI>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
