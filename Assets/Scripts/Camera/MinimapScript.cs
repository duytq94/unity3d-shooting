using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{

	private GameObject knight;

	void Start ()
	{
		knight = GameObject.FindGameObjectWithTag ("Knight");
	}

	void LateUpdate ()
	{
		Vector3 newPos = knight.transform.position;
		newPos.y = this.transform.position.y;
		this.transform.position = newPos;
		this.transform.rotation = Quaternion.Euler (90f, knight.transform.eulerAngles.y, 0f);
	}
}
