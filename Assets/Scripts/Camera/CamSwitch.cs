using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamSwitch : MonoBehaviour
{
	public GameObject attackButton;
	public GameObject runfastButton;
	public GameObject missileButton;
	public GameObject gunButton;

	private Camera camKnight;
	private Camera camAirCraft;
	private GameObject crosshair;

	void Start ()
	{
		crosshair = GameObject.FindGameObjectWithTag ("GUI").transform.GetChild (0).gameObject;
		camKnight = GameObject.FindGameObjectWithTag ("KnightCamera").GetComponent<Camera> ();
		camAirCraft = GameObject.FindGameObjectWithTag ("AircraftCamera").GetComponent<Camera> ();
	}

	public void SwitchPlayerButtonClick ()
	{
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

		attackButton.SetActive (true);
		runfastButton.SetActive (true);
		gunButton.SetActive (false);
		missileButton.SetActive (false);

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

		attackButton.SetActive (false);
		runfastButton.SetActive (false);
		gunButton.SetActive (true);
		missileButton.SetActive (true);

		crosshair.SetActive (true);
	}
}
