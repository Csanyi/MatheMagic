using Assets.Scripts.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class MapSceneBase : MonoBehaviour
{
	[SerializeField] private Canvas mapCanvas;
	[SerializeField] private TextMeshProUGUI xpText;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private Slider xpSlider;

	protected Database db;
	protected User user;

	protected virtual async void Awake()
	{
		mapCanvas.sortingOrder -= 1;
		db = new Database();
		user = await db.GetUserAsync();

		FillXpSlider();
	}

	private void FillXpSlider()
	{
		levelText.text = (user.Xp / 100).ToString();
		xpText.text = $"{user.Xp % 100}/100";
		xpSlider.value = user.Xp % 100;
	}
}
