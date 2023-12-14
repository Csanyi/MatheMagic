using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.PointerEventData;

public class ColorScript : MonoBehaviour
{

    public Coloring ColoringLevel;
    public GameObject DispExercise;
    public GameObject Palette;

    private GameObject[] fragments;
    private GameObject selectedFragment;
    private Button[] paints;
    private int selectedFragmentInd;
    private float width;
    private float height;

    private Color lightBlue = new Color(0.5f, 0.5f, 1f);
    private Dictionary<ColorCode, Color> colorCode2Color = new Dictionary<ColorCode, Color>(){
        {ColorCode.RED, Color.red},
        {ColorCode.GREEN, Color.green},
        {ColorCode.BLUE, new Color(0.5f, 0.7f, 1f)},
        {ColorCode.YELLOW, Color.yellow},
        {ColorCode.ORANGE, Color.yellow},
        {ColorCode.PURPLE, Color.blue},
        {ColorCode.BLACK, Color.black},
        {ColorCode.WHITE, new Color(0.8f, 0.95f, 1f)},
        {ColorCode.GRAY, Color.gray},
        {ColorCode.BROWN, new Color(0.75f, 0.65f, 0.5f)}};
    // Start is called before the first frame update

    public void PaletteOnClick(Button PaintButton)
    {
        int colInd = System.Array.IndexOf(paints, PaintButton);
        if ( selectedFragment.GetComponent<FragScript>().color == PaintButton.GetComponent<FragScript>().color)
        {
            ColoringLevel.SelectColor(colInd);
            ColoringLevel.ColorField(selectedFragmentInd);
            selectedFragment.GetComponent<SpriteRenderer>().color = colorCode2Color[selectedFragment.GetComponent<FragScript>().color];
        }
    }

    void Start()
    {
        List<ColorCode> ColorList = new List<ColorCode>();

        fragments = GameObject.FindGameObjectsWithTag("Color Field");
        paints = Palette.GetComponentsInChildren<Button>();
        Debug.Log(paints.Length);
        for (int i = 0; i < fragments.Length; i++)
        {
            ColorList.Add(fragments[i].GetComponent<FragScript>().color);
        }

        ColoringLevel = new Coloring(ColorList, Grade.FIRST);
        //we need a getter in the game logic class
        ColorList = ColorList.Distinct().ToList();
        for (int i = 0;i < ColorList.Count;i++)
        {
            paints[i].GetComponent<FragScript>().color = ColorList[i];
            paints[i].GetComponent<Image>().color = colorCode2Color[ColorList[i]];
        }

        for (int i = 0; i <paints.Length; i++)
        {
            Button temp = paints[i];
            paints[i].GetComponent<Button>().onClick.AddListener(delegate { PaletteOnClick(temp); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            var ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);
            if (hit.collider != null)
            {
                GameObject tempFragment = hit.collider.gameObject;
                int tempInd = System.Array.IndexOf(fragments, tempFragment);
                if (!ColoringLevel.IsFieldColored(tempInd))
                {
                    if (selectedFragment != null && !ColoringLevel.IsFieldColored(selectedFragmentInd))
                    { selectedFragment.GetComponent<SpriteRenderer>().color = Color.white; }
                    selectedFragment = tempFragment;
                    selectedFragmentInd = tempInd;
                    selectedFragment.GetComponent<SpriteRenderer>().color = Color.gray;
                    DispExercise.GetComponentInChildren<TextMeshProUGUI>().text = ColoringLevel.GetFieldLabel(selectedFragmentInd).ExerciseStringWithoutResult();
                }
                
            }
        }
    }
}