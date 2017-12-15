using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

	public GameObject explosionEffect;
	public float radiusExplosion = 5f;
	public float forceExplosion = 1500f;

	void Update ()
	{
		transform.Rotate (new Vector3 (0f, 0f, 20f));
		// Destroy if missile don't trigger anything
		Destroy (gameObject, 20f);
	}

	void OnTriggerEnter (Collider other)
	{
		Explode ();
		FindObjectOfType<AudioManager> ().PlayCountinuous ("Explosion");
	}

	public void Explode ()
	{
		Destroy (Instantiate (explosionEffect, transform.position, transform.rotation), 2f);
		Collider[] colliders = Physics.OverlapSphere (transform.position, radiusExplosion);
		foreach (Collider nearbyObject in colliders) {
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody> ();
			if (rb != null) {
				rb.AddExplosionForce (forceExplosion, transform.position, radiusExplosion);
			}

			if (nearbyObject.gameObject.tag == "Skeleton") {
				SkeletonController skeletonController = nearbyObject.gameObject.GetComponent<SkeletonController> ();
				skeletonController.BeMissileAttack (forceExplosion);
			}
		}

		Destroy (gameObject);
	}
}
