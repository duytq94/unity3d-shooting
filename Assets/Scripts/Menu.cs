using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{

	public Button btnPlay;
	public Button btnSetting;

	void Start ()
	{
		btnPlay.onClick.AddListener (OnBtnPlayDown);
		btnSetting.onClick.AddListener (OnBtnSettingDown);
	}

	public void OnBtnPlayDown ()
	{
		SceneManager.LoadSceneAsync ("Main");
	}

	public void OnBtnSettingDown ()
	{

	}
}
