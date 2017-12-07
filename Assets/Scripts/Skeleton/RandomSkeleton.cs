using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkeleton : MonoBehaviour
{
	// Plan zombie will born on it
	public GameObject plan;

	// Model zombie
	public GameObject modelSkeleton;

	// Player
	public GameObject player;

	// Skeleton distance at least from player
	public float distanceFromPlayer = 10f;

	// Density of quantity zombie (0 -> 1)
	public float density = 0.1f;

	// Radius of Sphere, 2f is quit fit with surround body zombie
	private float radius = 2f;

	private int maxSkeleton;
	private GameObject[] arraySkeleton;
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
		maxSkeleton = (int)((area / (radius * 20)) * density);
	}

	void Update ()
	{
		arraySkeleton = GameObject.FindGameObjectsWithTag ("Skeleton");
		if (arraySkeleton.Length < maxSkeleton) {
			Vector3 spawnPosition = new Vector3 (Random.Range (bounds.min.x, bounds.max.x), 0, Random.Range (bounds.min.z, bounds.max.z));
			Collider[] overlapCollider = Physics.OverlapSphere (spawnPosition, radius);

			// <= 1 mean zombie only colliding with ground
			if (overlapCollider.Length <= 1) {
				// if zombie position too near, we'll not create it
				if (Vector3.Distance (player.transform.position, spawnPosition) > distanceFromPlayer) {
					Instantiate (modelSkeleton, 
						spawnPosition, 
						Quaternion.Euler (modelSkeleton.transform.rotation.x, Random.rotation.y, modelSkeleton.transform.rotation.z));
				}
			}
		}
	}
}

