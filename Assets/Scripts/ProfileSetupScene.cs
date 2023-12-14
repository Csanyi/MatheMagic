using Assets.Scripts.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileSetupScene : MonoBehaviour
{
	[SerializeField] private Button startButton;
	[SerializeField] private Button avatarRightButton;
	[SerializeField] private Button avatarLeftButton;
	[SerializeField] private Image avatarImage;
	[SerializeField] private TMP_InputField nameField;
	[SerializeField] private Sprite maleSpirte;
	[SerializeField] private Sprite femaleSprite;
	[SerializeField] private GameObject inputErrorText;
	[SerializeField] private Button gradeRightButton;
	[SerializeField] private Button gradeLeftButton;
	[SerializeField] private TextMeshProUGUI gradeText;

	private User user;

	private void Awake()
	{
		user = new User();

		startButton.enabled = false;
		gradeText.text = $"{user.Class}. osztály";

		nameField.onDeselect.AddListener((name) => ValidateInput(name));
		avatarRightButton.onClick.AddListener(ArrowClick);
		avatarLeftButton.onClick.AddListener(ArrowClick);
		gradeRightButton.onClick.AddListener(() => GradeButtonClick(true));
		gradeLeftButton.onClick.AddListener(() => GradeButtonClick(false));
		startButton.onClick.AddListener(StartButtonClick);
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
			startButton.enabled = false;
		}
		else
		{
			user.Name = name;
			inputErrorText.SetActive(false);
			startButton.enabled = true;
		}
	}

	private void GradeButtonClick(bool up)
	{
		user.Class = up ? (user.Class % 4) + 1 : ((user.Class + 2) % 4) + 1;
		gradeText.text = $"{user.Class}. osztály";
	}

	private async void StartButtonClick()
	{
		await (new Database().CreateUserAsync(user));
		SceneManager.LoadScene(2);
	}
}
