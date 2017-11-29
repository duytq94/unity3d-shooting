using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombie : MonoBehaviour
{
	// Plan zombie will born on it
	public GameObject plan;

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
	private Bounds bounds;

	void Start ()
	{
		bounds = plan.GetComponent<Renderer> ().bounds;

		float width = bounds.size.x;
		float height = bounds.size.z;

		float area = width * height;
		if (density <= 0 || density > 1) {
			density = Random.Range (0, 1);
		}
		maxZombie = (int)((area / (radius * 20)) * density);
	}

	void Update ()
	{
		arrayZombie = GameObject.FindGameObjectsWithTag ("Zombie");
		if (arrayZombie.Length < maxZombie) {
			Vector3 spawnPosition = new Vector3 (Random.Range (bounds.min.x, bounds.max.x), 0, Random.Range (bounds.min.z, bounds.max.z));
			Collider[] overlapCollider = Physics.OverlapSphere (spawnPosition, radius);

			// <= 1 mean zombie only colliding with ground
			if (overlapCollider.Length <= 1) {
				// if zombie position too near, we'll not create it
				if (Vector3.Distance (player.transform.position, spawnPosition) > distanceFromPlayer) {
					Instantiate (modelZombie, 
						spawnPosition, 
						Quaternion.Euler (modelZombie.transform.rotation.x, Random.rotation.y, modelZombie.transform.rotation.z));
				}
			}
		}
	}
}

