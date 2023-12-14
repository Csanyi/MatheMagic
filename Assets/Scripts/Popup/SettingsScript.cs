using Assets.Scripts.Persistence;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
	[SerializeField] private Button rightButton;
	[SerializeField] private Button leftButton;
	[SerializeField] private TextMeshProUGUI gradeText;
	[SerializeField] private Button closeButton;
	[SerializeField] private Button saveButton;
	[SerializeField] private GameObject settingsPopup;

	private Database db;
	private User user;
	private int grade;

	private async void Start()
	{
		db = new Database();
		user = await db.GetUserAsync();

		grade = user.Class;
		gradeText.text = $"{grade}. osztály";
		
		rightButton.onClick.AddListener(() => GradeButtonClick(true));
		leftButton.onClick.AddListener(() => GradeButtonClick(false));
		saveButton.onClick.AddListener(SaveButtonClick);
		closeButton.onClick.AddListener(CloseButtonClick);
	}

	private void GradeButtonClick(bool up)
	{
		grade = up ? (grade % 4) + 1 : ((grade + 2) % 4) + 1;
		gradeText.text = $"{grade}. osztály";
	}

	private async void SaveButtonClick()
	{
		user.Class = grade;
		await db.UpdateUserAsync(user);
		settingsPopup.SetActive(false);
	}

	private void CloseButtonClick()
	{
		grade = user.Class;
		settingsPopup.SetActive(false);
		gradeText.text = $"{grade}. osztály";
	}
}
