using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour
{
	public float health = 50f;
	public float speed = 0.5f;

	private GameObject target;
	private float timeTemp;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		timeTemp = Time.time;
	}

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

	public void TakeDamage (float amount)
	{
		health -= amount;
		if (health <= 0) {
			Die ();
		}
	}

	public void Die ()
	{
		Animation animation = gameObject.GetComponentInChildren<Animation> ();
		animation ["Zombie_Death_01"].wrapMode = WrapMode.Once;
		animation.Play ("Zombie_Death_01");

		Destroy (gameObject.GetComponent<ZombieControl> ());
		Destroy (gameObject, 5);
	}
}
