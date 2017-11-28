using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombie : MonoBehaviour
{
	// Max and min in axis X and axis Y of ground
	public Vector3 minEdge;
	public Vector3 maxEdge;

	// Model zombie
	public GameObject modelZombie;

	// Player
	public GameObject player;

	// Zombie distance at least from player
	public float distanceFromPlayer = 10f;

	// Density of quantity zombie (0 -> 1)
	public float density = 0.1f;

	// Radius of Sphere, 2f is quit fit with surround body zombie
	private float radius = 2f;

	private int maxZombie;
	private GameObject[] arrayZombie;

	// Use this for initialization
	void Start ()
	{
		int width = (int)(maxEdge.x - minEdge.x);
		int height = (int)(maxEdge.z - minEdge.z);
		int area = width * height;
		if (density <= 0 || density > 1) {
			density = Random.Range (0, 1);
		}
		maxZombie = (int)((area / (radius * 10)) * density);
	}
	
	// Update is called once per frame
	void Update ()
	{
		arrayZombie = GameObject.FindGameObjectsWithTag ("Zombie");
		if (arrayZombie.Length < maxZombie) {
			Vector3 spawnPosition = new Vector3 ((int)Random.Range (minEdge.x, maxEdge.x), 0, (int)Random.Range (minEdge.z, maxEdge.z));
			Collider[] overlapCollider = Physics.OverlapSphere (spawnPosition, radius);

			// <= 1 mean zombie only colliding with ground
			if (overlapCollider.Length <= 1) {
				// if zombie position too near, we'll not create it
				if (Vector3.Distance (player.transform.position, spawnPosition) > distanceFromPlayer) {
					Instantiate (modelZombie, 
						spawnPosition + transform.TransformPoint (0, 0, 0), 
						Quaternion.Euler (modelZombie.transform.rotation.x, Random.rotation.y, modelZombie.transform.rotation.z));
				}
			}
		}
	}
}

