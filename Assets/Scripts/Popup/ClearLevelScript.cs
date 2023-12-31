using Assets.Scripts.Persistence;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearLevelScript : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private Slider xpSlider;

    public int SceneToLoad { get; set; }

    private Database db;
    private User user;
    

    private void Awake()
    {
        homeButton.onClick.AddListener(() => SceneManager.LoadScene(4));
        nextButton.onClick.AddListener(() => SceneManager.LoadScene(SceneToLoad));
    }

	public async Task TaskCompleted()
	{
		db = new Database();
		user = await db.GetUserAsync();

		user.Xp += 25;
		levelText.text = (user.Xp / 100).ToString();
		xpSlider.value = (user.Xp % 100);
		xpText.text = "+25 XP";
		await db.UpdateUserAsync(user);
	}
}
