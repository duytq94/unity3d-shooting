using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamSwitch : MonoBehaviour
{
	private Camera camKnight;
	private Camera camAirCraft;
	private GameObject crosshair;

	void Start ()
	{
		crosshair = GameObject.FindGameObjectWithTag ("GUI").transform.GetChild (0).gameObject;
		camKnight = GameObject.FindGameObjectWithTag ("KnightCamera").GetComponent<Camera> ();
		camAirCraft = GameObject.FindGameObjectWithTag ("AircraftCamera").GetComponent<Camera> ();
	}

	void Update ()
	{
		// For mouse click
		if (Input.GetButtonDown ("Fire2")) {
			if (camKnight.isActiveAndEnabled) {
				CamAirCraftActive ();
			} else {
				CamKnightActive ();
			}
		}
	}

	public void SwitchPlayerButtonClick ()
	{
		// For touch on mobile
		if (camKnight.isActiveAndEnabled) {
			CamAirCraftActive ();
		} else {
			CamKnightActive ();
		}
	}

	public void CamKnightActive ()
	{
		camKnight.enabled = true;
		camAirCraft.enabled = false;

		if (SceneManager.GetActiveScene ().name == "Level1") {
			FindObjectOfType<AudioManager> ().Play ("DesertWind");
			FindObjectOfType<AudioManager> ().Pause ("Helicopter");
		} else if (SceneManager.GetActiveScene ().name == "Level2") {
			FindObjectOfType<AudioManager> ().Play ("Cemetery");
			FindObjectOfType<AudioManager> ().Pause ("Helicopter");
		}


		crosshair.SetActive (false);
	}

	public void CamAirCraftActive ()
	{
		camKnight.enabled = false;
		camAirCraft.enabled = true;

		if (SceneManager.GetActiveScene ().name == "Level1") {
			FindObjectOfType<AudioManager> ().Pause ("DesertWind");
			FindObjectOfType<AudioManager> ().Play ("Helicopter");
		} else if (SceneManager.GetActiveScene ().name == "Level2") {
			FindObjectOfType<AudioManager> ().Pause ("Cemetery");
			FindObjectOfType<AudioManager> ().Play ("Helicopter");
		}

		crosshair.SetActive (true);
	}
}
