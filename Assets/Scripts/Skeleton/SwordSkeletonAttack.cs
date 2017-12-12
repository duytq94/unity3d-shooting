using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkeletonAttack : MonoBehaviour
{
	public float damAttack = 10f;
	public GameObject skeleton;

	void OnTriggerEnter (Collider col)
	{
		print ("aaa");
		if (col.tag == "Knight" && skeleton.GetComponent<SkeletonController> ().GetIsAllive ()
		    && skeleton.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			print ("bbb");
			col.gameObject.GetComponent<KnightController> ().BeAttack (damAttack);
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.tag == "Knight") {
			col.gameObject.GetComponent<KnightController> ().ExitAttack ();
		}
	}
}
