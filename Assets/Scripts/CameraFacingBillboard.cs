using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
	private GameObject airCraftCamera;
	private GameObject wrapAircraftCamera;
	private GameObject knight;

	void Start ()
	{
		wrapAircraftCamera = GameObject.FindGameObjectWithTag ("WrapAircraftCamera");
		airCraftCamera = wrapAircraftCamera.transform.GetChild (0).gameObject;
		knight = GameObject.FindGameObjectWithTag ("Knight");
	}

	void Update ()
	{
		if (airCraftCamera.GetComponent<Camera> ().isActiveAndEnabled) {
			gameObject.transform.LookAt (wrapAircraftCamera.transform.position);
		} else {
			gameObject.transform.LookAt (knight.transform.position);
		}
	}
}

