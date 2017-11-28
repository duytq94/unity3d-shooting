using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTarget : MonoBehaviour
{

	public float health = 50f;

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

		Destroy (gameObject.GetComponent<ZombieAutoMove> ());
		Destroy (gameObject, 5);
			
	}
}
