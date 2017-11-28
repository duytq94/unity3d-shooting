using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAutoMove : MonoBehaviour
{

	public GameObject target;
	public float speed = 0.5f;

	// Update is called once per frame
	void Update ()
	{
		float step = speed * Time.deltaTime;

		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);

		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0F);
		transform.rotation = Quaternion.LookRotation (newDir);
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			GetComponentInChildren<Animation> ().Play ("Zombie_Attack_01");
		}
	}

	void OnCollisionExit (Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			GetComponentInChildren<Animation> ().Play ("Zombie_Walk_01");
		}
	}
}
