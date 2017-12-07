using UnityEngine;
using System.Collections;

public class GunAircraft : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 60f;

	public GameObject crosshair;
	public GameObject impactEffect;

	public Camera[] cams;

	private float timeTemp;
	private bool click;

	void Update ()
	{
		if (GetComponent<Camera> ().isActiveAndEnabled) {
			if (Input.GetButtonDown ("Fire1")) {
				timeTemp = Time.time;
				click = true;
			}
			
			// Long click
			if (click && (Time.time - timeTemp) > 0.2) {
				FindObjectOfType<AudioManager> ().PlayCountinuous ("ShotHandGun");
				Shoot ();
			}

			// Short click
			if (Input.GetButtonUp ("Fire1")) {
				click = false;
				if ((Time.time - timeTemp) < 0.2) {
					FindObjectOfType<AudioManager> ().PlayCountinuous ("ShotHandGun");
					Shoot ();
				}
			}
		}
	}

	void Shoot ()
	{
		RaycastHit hit;
		if (Physics.Raycast (GetComponent<Camera> ().transform.position, GetComponent<Camera> ().transform.forward, out hit, range)) {
			SkeletonController target = hit.transform.GetComponent<SkeletonController> ();

			if (target != null) {
				target.BeGunAttack (damage);
			}

			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.5f);
		}
	}
}
