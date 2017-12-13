using UnityEngine;
using System.Collections;

public class GunHelicopter : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 10f;
	public float speedMissile = 800f;

	public GameObject crosshair;
	public GameObject impactEffect;
	public GameObject bloodParticles;
	public GameObject helicopter;
	public GameObject missile;

	public Camera[] cams;

	private float timeTempGun;
	private float timeTempMissile;
	private bool click;

	void Update ()
	{
		if (GetComponent<Camera> ().isActiveAndEnabled) {
			if (Input.GetButtonDown ("Fire1")) {
				timeTempGun = Time.time;
				click = true;
			}

			// Missile
			if (Input.GetKeyDown ("space") && (Time.time - timeTempMissile > 0.5)) {
				FindObjectOfType<AudioManager> ().PlayCountinuous ("MissileFire");
				timeTempMissile = Time.time;
				ShootMissile ();
			}
			
			// Long click
			if (click && (Time.time - timeTempGun) > 0.2) {
				FindObjectOfType<AudioManager> ().PlayCountinuous ("ShotHandGun");
				ShootGun ();
			}

			// Short click
			if (Input.GetButtonUp ("Fire1")) {
				click = false;
				if ((Time.time - timeTempGun) < 0.2) {
					FindObjectOfType<AudioManager> ().PlayCountinuous ("ShotHandGun");
					ShootGun ();
				}
			}
		}
	}

	public void ShootGun ()
	{
		RaycastHit hit;
		if (Physics.Raycast (GetComponent<Camera> ().transform.position, GetComponent<Camera> ().transform.forward, out hit, range)) {
			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			SkeletonController skeletonController = hit.transform.GetComponent<SkeletonController> ();
			// Shoot to skeleton
			if (skeletonController != null && skeletonController.GetIsAllive ()) {
				skeletonController.BeGunAttack (damage);
				GameObject blood = Instantiate (bloodParticles, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (blood, 1f);
			} else {
				// Shoot to something else
				GameObject impact = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
				Destroy (impact, 0.5f);
			}
		}
	}

	public void ShootMissile ()
	{
		GameObject missileModel = Instantiate (missile);
		Rigidbody rb = missileModel.GetComponent<Rigidbody> ();
		missileModel.transform.rotation = GetComponent<Camera> ().transform.rotation;
		missileModel.transform.position = helicopter.transform.position;
		rb.AddForce (GetComponent<Camera> ().transform.forward * speedMissile);
	}
}
