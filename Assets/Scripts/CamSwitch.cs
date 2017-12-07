using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
	public Camera camKnight;
	public Camera camAirCraft;
	private GameObject crosshair;

	void Start ()
	{
		crosshair = GameObject.FindGameObjectWithTag ("GUI").transform.GetChild (0).gameObject;
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire2")) {
			if (camKnight.isActiveAndEnabled) {
				CamAirCraftActive ();
			} else {
				CamKnightActive ();
			}
		}
	}

	public void CamKnightActive ()
	{
		camKnight.enabled = true;
		camAirCraft.enabled = false;

		FindObjectOfType<AudioManager> ().Play ("DesertWind");
		FindObjectOfType<AudioManager> ().Pause ("Helicopter");

		crosshair.SetActive (false);
	}

	public void CamAirCraftActive ()
	{
		camKnight.enabled = false;
		camAirCraft.enabled = true;

		FindObjectOfType<AudioManager> ().Pause ("DesertWind");
		FindObjectOfType<AudioManager> ().Play ("Helicopter");

		crosshair.SetActive (true);
	}
}
