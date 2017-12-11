using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

	public GameObject loadingScreen;
	public Slider sliderLoading;
	public Slider sliderVolume;
	public Text progressText;

	private string keyVolume = "Volume";

	public void Start ()
	{
		if (sliderVolume != null) {
			if (PlayerPrefs.GetFloat ("Volume", -1f) != -1f) {
				sliderVolume.value = PlayerPrefs.GetFloat (keyVolume);
			} else {
				sliderVolume.value = 0.5f;
			}
			AudioListener.volume = sliderVolume.value;
		}
	}

	public void LoadLevel (string sceneName)
	{
		StartCoroutine (LoadAsynchronously (sceneName));
	}

	public void SetVolume (float volume)
	{
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat (keyVolume, volume);
	}

	IEnumerator LoadAsynchronously (string sceneName)
	{
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync (sceneName);
		loadingScreen.SetActive (true);

		while (!asyncOperation.isDone) {
			// Cause progress just return value form 0 to 0.9, we convert to 0 -> 1
			float progress = Mathf.Clamp01 (asyncOperation.progress / 0.9f);

			sliderLoading.value = progress;
			progressText.text = string.Format ("{0}%", Mathf.Round (progress * 100f));
			yield return null;
		}
	}

	public void QuitApp ()
	{
		Application.Quit ();	
	}
}
