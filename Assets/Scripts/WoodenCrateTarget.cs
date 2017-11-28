using UnityEngine;

public class WoodenCrateTarget : MonoBehaviour
{

	public float health = 50f;
	public GameObject destroyedVersion;

	public void TakeDamage (float amount)
	{
		health -= amount;
		if (health <= 0) {
			Die ();
		}
	}

	void Die ()
	{
		Destroy (gameObject);
		Instantiate (destroyedVersion, transform.position, transform.rotation);
	}
}
