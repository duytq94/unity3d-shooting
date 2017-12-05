using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour
{
	public float health = 50f;
	public float speed = 1f;

	private Animator anim;
	private GameObject knight;
	private bool isAllive = true;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		knight = GameObject.FindGameObjectWithTag ("Knight");
		print (knight);
	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			return;
		}

		Vector3 direction = knight.transform.position - this.transform.position;
		if (knight.GetComponent<KnightController> ().GetIsAllive ()) {
			direction.y = 0;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool ("isIdle", false);

			if (direction.magnitude > 0.1f) {
				this.transform.Translate (0, 0, speed / 2000f);
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);
			} else {
				anim.SetBool ("isAttacking", true);
				anim.SetBool ("isWalking", false);
			}

		} else {
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
		}
	}

	public void BeAttack (float damAttack)
	{
		health -= damAttack;
		anim.SetBool ("isDamage", true);
		if (health <= 0) {
			anim.SetBool ("isDead", true);
			isAllive = false;
		}
	}

	public void BeGunAttack (float damAttack)
	{
		health -= damAttack;
		anim.SetBool ("isDamage", true);
		if (health <= 0) {
			anim.SetBool ("isDead", true);
			isAllive = false;
		}
		StartCoroutine (Wait ());
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (0.25f);
		anim.SetBool ("isDamage", false);
	}

	public void ExitAttack ()
	{
		anim.SetBool ("isDamage", false);
	}

	public bool GetIsAllive ()
	{
		return isAllive;
	}
}
