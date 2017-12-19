using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{

	public float speed = 10f;

	private GameObject camera;
	private VirtualJoystick joystick;

	void Start ()
	{
		camera = transform.GetChild (0).gameObject;	
		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<VirtualJoystick> ();
	}

	void Update ()
	{
		if (camera.GetComponent<Camera> ().isActiveAndEnabled) {
			float translation = joystick.Vertical () * speed;
			float straffe = joystick.Horizontal () * speed;

			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate (straffe, 0, translation);
		}
	}
}
