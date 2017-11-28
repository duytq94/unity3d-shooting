using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombie : MonoBehaviour
{
	// Max and min in axis X and axis Y of ground
	public Vector3 minPosition;
	public Vector3 maxPosition;

	// Model zombie
	public GameObject modelZombie;

	// Density of quantity zombie (0 -> 1)
	public float density = 0.1f;

	// Radius of Sphere
	public float radius = 2f;

	private int maxZombie;
	private GameObject[] arrayZombie;

	// Use this for initialization
	void Start ()
	{
		int width = (int)(maxPosition.x - minPosition.x);
		int height = (int)(maxPosition.z - minPosition.z);
		int area = width * height;
		if (density <= 0 || density > 1) {
			density = Random.Range (0, 1);
		}
		maxZombie = (int)((area / 10) * density);
	}
	
	// Update is called once per frame
	void Update ()
	{
		arrayZombie = GameObject.FindGameObjectsWithTag ("Zombie");
		if (arrayZombie.Length < maxZombie) {
			Vector3 spawnPosition = new Vector3 ((int)Random.Range (minPosition.x, maxPosition.x), 0, (int)Random.Range (minPosition.z, maxPosition.z));
			Collider[] overlapCollider = Physics.OverlapSphere (spawnPosition, radius);

			// <= 1 mean zombie only colliding with ground
			if (overlapCollider.Length <= 1) {
				Instantiate (modelZombie, 
					spawnPosition + transform.TransformPoint (0, 0, 0), 
					Quaternion.Euler (modelZombie.transform.rotation.x, Random.rotation.y, modelZombie.transform.rotation.z));	
			}
		}
	}
}

