using Assets.Scripts.Persistence;
using Firebase;
using Firebase.Analytics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMapScene : MapSceneBase
{
	[SerializeField] private Button operationMapButton;
	[SerializeField] private Button profileButton;

	protected override void Awake()
	{
		base.Awake();
		operationMapButton.onClick.AddListener(() => SceneManager.LoadScene(2));
		profileButton.onClick.AddListener(() => SceneManager.LoadScene(1));		
	}
}
