using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

	public float speed = 10f;
	private GameObject camera;

	void Start ()
	{
		camera = transform.GetChild (0).gameObject;	
	}

	void Update ()
	{
		if (camera.GetComponent<Camera> ().isActiveAndEnabled) {
			float translation = Input.GetAxis ("Vertical") * speed;
			float straffe = Input.GetAxis ("Horizontal") * speed;

			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate (straffe, 0, translation);

		}
	}
}
