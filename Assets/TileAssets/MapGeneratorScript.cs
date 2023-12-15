using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Persistence;

public class MapGeneratorScript : MonoBehaviour
{

    public int rows;
    public int cols;

    public int spriteWidth;
    public int spriteHeight;

    public GameObject tilePrefab;
    public Canvas mainCanvas;
    public TileGameLogicScript gameLogic;
    public GameObject DispExercise;

    public Sprite wrongAsnwerSprite;
    public Sprite correctAsnwerSprite;
    private Grade grade;
    private Tile TileLevel;

    [SerializeField] private GameObject clearLevelPopup;

    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    async void Start()
    {
        var db = new Database();
        User user = await db.GetUserAsync();

        canvas.sortingOrder -= 1;

        if (user  is not null)
        {
            grade = (Grade)(user.Class - 1);
            TileLevel = new Tile(rows, cols, true, grade);
            Vector3 offset = new Vector3(-400, -300, 0);
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
                    newTile.GetComponentInChildren<TextMeshProUGUI>().text = TileLevel.GetNumberOnTile((x, y)).ToString();
                }
            }
            DispExercise.GetComponentInChildren<TextMeshProUGUI>().text = TileLevel.GetCurrentRule();
            (int, int) firstTile = TileLevel.GetCurrentTile();
            GameObject currTile = GameObject.Find("Tile_" + firstTile.Item1.ToString() + firstTile.Item2.ToString());
            currTile.GetComponent<Image>().sprite = correctAsnwerSprite;
            currTile.GetComponentInChildren<TextMeshProUGUI>().text = "";

        }
        else
        {
            Debug.LogWarning("User is null.");
            // some kind of error popup
        }


    }

    async public void tileOnClick(GameObject tile, int x, int y)
    {
        Debug.Log(tile.name + " at " + x.ToString() + ", " + y.ToString() + " was clicked.");
        (int, int) current = TileLevel.GetCurrentTile();
        int diffx = System.Math.Abs(current.Item1 - x);
        int diffy = System.Math.Abs(current.Item2 - y);
        if (((diffx == 0 && diffy == 1) || (diffx == 1 && diffy == 0)) && ! TileLevel.GetTileIsVisited((x, y)))
        {
            if (TileLevel.StepOnTile((x, y)))
            {
                tile.GetComponent<Image>().sprite = correctAsnwerSprite;
                if (TileLevel.IsFinished())
                {
                    ClearLevelScript script = clearLevelPopup.GetComponentInChildren<ClearLevelScript>();
                    script.ScenetToLoad = 5;
                    clearLevelPopup.SetActive(true);
                    await script.TaskCompleted();
                }
                else
                {
                    DispExercise.GetComponentInChildren<TextMeshProUGUI>().text = TileLevel.GetCurrentRule();
                }
            }
            else
            {
                tile.GetComponent<Image>().sprite = wrongAsnwerSprite;
            }
            tile.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
