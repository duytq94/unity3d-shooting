using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public Dropdown resDropdown;
	private Resolution[] resolutions;

	private int quantityAlive;
	private int quantityDead;
	private Text quantityAliveBoard;
	private Text quantityDeadBoard;

	// Use this for initialization
	void Start ()
	{
		quantityAliveBoard = GameObject.FindGameObjectWithTag ("QuantityAlive").GetComponent<Text> ();
		quantityDeadBoard = GameObject.FindGameObjectWithTag ("QuantityDead").GetComponent<Text> ();
		quantityAliveBoard.text = "0";
		quantityDeadBoard.text = "0";

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
	}
	
	// Update is called once per frame
	void Update ()
	{
		quantityAlive = GameObject.FindGameObjectsWithTag ("Skeleton").Length;
		quantityAliveBoard.text = quantityAlive.ToString ();
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
		} else {
			Time.timeScale = 1;
		}
	}

	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
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
