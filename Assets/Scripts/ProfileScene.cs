using Assets.Scripts.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileScene : MonoBehaviour
{
	[SerializeField] private Button rightButton;
	[SerializeField] private Button leftButton;
	[SerializeField] private Button homeButton;
	[SerializeField] private Button saveButton;
	[SerializeField] private Image avatarImage;
	[SerializeField] private TMP_InputField nameField;
	[SerializeField] private Sprite maleSpirte;
	[SerializeField] private Sprite femaleSprite;
	[SerializeField] private GameObject inputErrorText;
	[SerializeField] private Canvas canvas;

	private Database db;
	private User user;

	private async void Awake()
	{
		canvas.sortingOrder -= 1;

		db = new Database();
		user = await db.GetUserAsync();

		if (user is not null)
		{
			Setup();
		}
		else
		{
			// Some kind of error popup
		}
	}

	private void Setup()
	{
		nameField.text = user.Name;
		avatarImage.sprite = (user.Character == Characters.Male) ? maleSpirte : femaleSprite;
		avatarImage.color = Color.white;

		nameField.onDeselect.AddListener((name) => ValidateInput(name));

		homeButton.onClick.AddListener(HomeButtonClick);
		saveButton.onClick.AddListener(SaveButtonClick);
		rightButton.onClick.AddListener(ArrowClick);
		leftButton.onClick.AddListener(ArrowClick);
	}

	private void ArrowClick()
	{
		switch (user.Character)
		{
			case Characters.Male:
				avatarImage.sprite = femaleSprite;
				user.Character = Characters.Female;
				break;
			case Characters.Female:
				avatarImage.sprite = maleSpirte;
				user.Character = Characters.Male;
				break;
		}
	}

	private void ValidateInput(string name)
	{
		if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
		{
			inputErrorText.SetActive(true);
		}
		else
		{
			user.Name = name;
			inputErrorText.SetActive(false);
		}
	}

	private async void SaveButtonClick()
	{
		await db.UpdateUserAsync(user);
	}

	private void HomeButtonClick()
	{
		SceneManager.LoadScene(2);
	}
}
