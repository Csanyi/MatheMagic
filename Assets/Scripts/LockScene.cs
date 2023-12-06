using Assets.Scripts.Persistence;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LockScene : MonoBehaviour
{
	[SerializeField] private GameObject tilePrefab;

	[SerializeField] private GameObject resultPrefab;
	[SerializeField] private Canvas canvas;

	[SerializeField] private Sprite plusSignSprite;
	[SerializeField] private Sprite minusSignSprite;

	private int tileSize;
	private int lockDigitCnt;
	private Lock lockGame;

	private async void Start()
	{
		Database db = new Database();

		User user = await db.GetUserAsync();

		if (user is not null)
		{
			DrawTiles(user.Class);
		} else
		{
			Debug.LogWarning("User is null.");
			// some kind of error popup
		}
	}


	private void DrawTiles(int grade)
	{
		tileSize = (int)tilePrefab.GetComponent<RectTransform>().sizeDelta.x;
		lockGame = new Lock((grade == 4) ? 3 : grade);
		lockDigitCnt = lockGame.GetDigitCnt(lockGame.GetExercise().GetResult());
		Vector3 offset = new Vector3(-(lockDigitCnt - 1) * (tileSize / 2) + 40, -140, 0);

		int min = int.MaxValue;
		int x = lockDigitCnt - 1;
		int y = 2;

		// OPERAND 1
		foreach (int n in lockGame.GetDigits(lockGame.GetExercise().GetOperand1()))
		{
			GameObject tile = Instantiate(tilePrefab);
			tile.transform.SetParent(canvas.transform, false);
			tile.GetComponent<RectTransform>().localPosition = new Vector3(x * tileSize, y * tileSize, 0) + offset;
			tile.GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
			--x;
		}

		min = Math.Min(min, x);
		x = lockDigitCnt - 1;
		--y;

		// OPERAND 2
		foreach (int n in lockGame.GetDigits(lockGame.GetExercise().GetOperand2()))
		{
			GameObject tile = Instantiate(tilePrefab);
			tile.transform.SetParent(canvas.transform, false);
			tile.GetComponent<RectTransform>().localPosition = new Vector3(x * tileSize, y * tileSize, 0) + offset;
			tile.GetComponentInChildren<TextMeshProUGUI>().text = n.ToString();
			--x;
		}

		min = Math.Min(min, x);

		// SIGN
		GameObject sign = Instantiate(tilePrefab);
		sign.transform.SetParent(canvas.transform, false);
		sign.GetComponent<RectTransform>().localPosition = new Vector3(min * tileSize, y * tileSize, 0) + offset;

		switch (lockGame.GetExercise().GetOperationType())
		{
			case Operation.ADDITION:
				sign.GetComponentInChildren<Image>().sprite = plusSignSprite;
				break;
			case Operation.SUBTRACTION:
				sign.GetComponentInChildren<Image>().sprite = minusSignSprite;
				break;
		}

		// RESULT
		for (int i = lockDigitCnt - 1; i >=0 ; --i)
		{
			GameObject tile = Instantiate(resultPrefab);
			tile.transform.SetParent(canvas.transform, false);
			tile.GetComponent<RectTransform>().localPosition = new Vector3(i * tileSize, -20, 0) + offset;
			tile.name = $"ResultTile_{i}";

			TextMeshProUGUI text = GameObject.Find($"{tile.name}/Image").ConvertTo<Image>().GetComponentInChildren<TextMeshProUGUI>();
			text.text = lockGame.GetLockDigit(i).ToString();
			
			int index = i;
			GameObject.Find($"{tile.name}/UpButton").ConvertTo<Button>().onClick.AddListener(() => ResultButtonOnClick(text, index, true));
			GameObject.Find($"{tile.name}/DownButton").ConvertTo<Button>().onClick.AddListener(() => ResultButtonOnClick(text, index, false));
		}
	}

	private void ResultButtonOnClick(TextMeshProUGUI text, int index, bool up)
	{
		lockGame.RotateDigit(index, up);
		text.text = lockGame.GetLockDigit(index).ToString();

		if (lockGame.IsFinished())
		{
			DisableButtons();
			Debug.Log("FINISHED!");
		}
	}

	private void DisableButtons()
	{
		for (int i = 0; i < lockDigitCnt; ++i)
		{
			GameObject.Find($"ResultTile_{i}/UpButton").ConvertTo<Button>().enabled = false;
			GameObject.Find($"ResultTile_{i}/DownButton").ConvertTo<Button>().enabled = false;
		}
	}
}

