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

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}

		if (Input.GetButtonDown ("Fire2")) {
			if (cams [1].GetComponent<Camera> ().isActiveAndEnabled) {
				CamPlayerActive ();
			} else {
				CamAircraftActive ();
			}
		}
	}

	void Shoot ()
	{
		RaycastHit hit;
		if (Physics.Raycast (GetComponent<Camera> ().transform.position, GetComponent<Camera> ().transform.forward, out hit, range)) {
			ZombieTarget target = hit.transform.GetComponent<ZombieTarget> ();

			if (target != null) {
				target.TakeDamage (damage);
			}

			if (hit.rigidbody != null) {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}

			GameObject impactGO = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactGO, 0.5f);
		}
	}

	void CamPlayerActive ()
	{
		cams [0].GetComponent<Camera> ().enabled = true;
		cams [1].GetComponent<Camera> ().enabled = false;
		crosshair.SetActive (false);
	}

	void CamAircraftActive ()
	{
		cams [0].GetComponent<Camera> ().enabled = false;
		cams [1].GetComponent<Camera> ().enabled = true;
		crosshair.SetActive (true);
	}
}
