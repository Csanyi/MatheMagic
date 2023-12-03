using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Lock
{
	public class Lock : MonoBehaviour
	{
		[SerializeField] private GameObject tilePrefab;
		[SerializeField] private GameObject resultPrefab;
		[SerializeField] private Canvas canvas;

		[SerializeField] private Sprite plusSignSprite;
		[SerializeField] private Sprite minusSignSprite;

		[SerializeField] private int tileSize;

		private GameObject[,] tiles;

		private void Start()
		{
			tiles = new GameObject[3, 5]; ;
			Vector3 offset = new Vector3(-180, -140, 0);

			for (int i = 0; i < 2; ++i)
			{
				for (int j = 0; j < 4; ++j)
				{
					GameObject tile = Instantiate(tilePrefab);
					tile.transform.SetParent(canvas.transform, false);
					tile.GetComponent<RectTransform>().localPosition = new Vector3(j * tileSize, (2-i) * tileSize, 0) + offset;
					tile.name = $"({i},{j})";

					tiles[i, j] = tile;
				}
			}

			for (int i = 0; i < 5; ++i)
			{
				GameObject tile = Instantiate(resultPrefab);
				tile.transform.SetParent(canvas.transform, false);
				tile.GetComponent<RectTransform>().localPosition = new Vector3((i-1) * tileSize, -20, 0) + offset;

				tiles[2, i] = tile;
			}


			GameObject sign = Instantiate(tilePrefab);
			sign.transform.SetParent(canvas.transform, false);
			sign.GetComponent<RectTransform>().localPosition = new Vector3(-1 * tileSize, tileSize, 0) + offset;
			sign.GetComponentInChildren<Image>().sprite = plusSignSprite;
		}
	}
}
