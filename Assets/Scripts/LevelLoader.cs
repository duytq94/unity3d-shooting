using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

	public GameObject loadingScreen;
	public Slider slider;
	public Text progressText;

	public void LoadLevel (string sceneName)
	{
		StartCoroutine (LoadAsynchronously (sceneName));
	}

	IEnumerator LoadAsynchronously (string sceneName)
	{
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (sceneName);
		loadingScreen.SetActive (true);

		while (!asyncOperation.isDone) {
			// Cause progress just return value form 0 to 0.9, we convert to 0 -> 1
			float progress = Mathf.Clamp01 (asyncOperation.progress / 0.9f);

			slider.value = progress;
			progressText.text = string.Format ("{0}%", Mathf.Round (progress * 100f));
			yield return null;
		}
	}

	public void QuitApp ()
	{
		Application.Quit ();	
	}
}
