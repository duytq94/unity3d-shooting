using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour
{
	public float health = 50f;
	public float speed = 0.5f;

	private GameObject target;
	private float timeTemp;
	private bool beAttack = false;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
		timeTemp = Time.time;
	}

	void Update ()
	{
		float step = speed * Time.deltaTime;

		// Move
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, step);

		// Rotate
		Vector3 targetDir = target.transform.position - transform.position;
		transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (targetDir), 0.1f);
	}

	public void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			GetComponentInChildren<Animation> ().Play ("Zombie_Attack_01");
			beAttack = true;
		}
	}

	public void OnCollisionStay (Collision col)
	{
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerGetAttack> ().beAttack (0.05f);
		}
	}

	public void OnCollisionExit (Collision collision)
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
