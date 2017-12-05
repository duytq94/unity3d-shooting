using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnightAttack : MonoBehaviour
{
	public float damAttack = 20f;
	public GameObject knight;

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Skeleton" && knight.GetComponent<KnightController> ().GetIsAllive ()
		    && knight.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Attack1")) {
			col.gameObject.GetComponent<SkeletonController> ().BeAttack (damAttack);
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Skeleton") {
			col.gameObject.GetComponent<SkeletonController> ().ExitAttack ();
		}
	}
}
