using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Dropdown resDropdown;
	public Slider sliderVolume;
	private Resolution[] resolutions;

	private int quantityAlive;
	private int quantityDead;
	private Text quantityAliveBoard;
	private Text quantityDeadBoard;

	// Use this for initialization
	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		if (GameObject.FindGameObjectWithTag ("QuantityAlive") != null) {
			quantityAliveBoard = GameObject.FindGameObjectWithTag ("QuantityAlive").GetComponent<Text> ();
			quantityAliveBoard.text = "0";
		}
		if (GameObject.FindGameObjectWithTag ("QuantityDead") != null) {
			quantityDeadBoard = GameObject.FindGameObjectWithTag ("QuantityDead").GetComponent<Text> ();
			quantityDeadBoard.text = "0";
		}

		int currentResIndex = 0;
		resolutions = Screen.resolutions;
		resDropdown.ClearOptions ();
		List<string> options = new List<string> ();
		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);
			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResIndex = i;
			}
		}
		resDropdown.AddOptions (options);
		resDropdown.value = currentResIndex;
		resDropdown.RefreshShownValue ();

		if (sliderVolume != null) {
			if (PlayerPrefs.GetFloat ("Volume", -1f) != -1f) {
				sliderVolume.value = PlayerPrefs.GetFloat ("Volume");
			} else {
				sliderVolume.value = 0.5f;
			}
			AudioListener.volume = sliderVolume.value;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (quantityAliveBoard != null) {
			quantityAlive = GameObject.FindGameObjectsWithTag ("Skeleton").Length;
			quantityAliveBoard.text = quantityAlive.ToString ();
		}
		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void SetVolume (float volume)
	{
		AudioListener.volume = volume;
		PlayerPrefs.SetFloat ("Volume", volume);
	}

	public void SkeletonDead ()
	{
		quantityDead++;
		quantityDeadBoard.text = quantityDead.ToString ();
		if (quantityDead >= 5) {
			FindObjectOfType<LoadingManager> ().LoadLevel ("Level2");
		}
	}

	public void PauseGame (bool isPause)
	{
		if (isPause) {
			Time.timeScale = 0;
			FindObjectOfType<AudioManager> ().enabled = false;
			GameObject.FindGameObjectWithTag ("KnightCamera").GetComponent<MouseLookKnight> ().enabled = false;
			GameObject.FindGameObjectWithTag ("AircraftCamera").GetComponent<MouseLookAircraft> ().enabled = false;
			GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioListener> ().enabled = false;
		} else {
			Time.timeScale = 1;
			GameObject.FindGameObjectWithTag ("KnightCamera").GetComponent<MouseLookKnight> ().enabled = true;
			GameObject.FindGameObjectWithTag ("AircraftCamera").GetComponent<MouseLookAircraft> ().enabled = true;
			GameObject.FindGameObjectWithTag ("AudioManager").GetComponent<AudioListener> ().enabled = true;
		}
	}

	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
		print (qualityIndex);
	}

	public void SetFullScreen (bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
	}

	public void SetResolution (int resIndex)
	{
		Resolution res = resolutions [resIndex];
		Screen.SetResolution (res.width, res.height, Screen.fullScreen);
	}

}
