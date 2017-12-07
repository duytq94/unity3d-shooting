using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
	[Range (0f, 100f)]
	public float health = 100f;
	public Slider healthbar;
	public float speed = 1f;
	public GameObject knightView;

	private Animator anim;
	private bool isAllive = true;

	// Use this for initialization
	void Start ()
	{
		healthbar.value = health;
		anim = GetComponent<Animator> ();
		Cursor.lockState = CursorLockMode.Locked;
		speed = speed / 20f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			return;
		}

		if (knightView.GetComponent<Camera> ().isActiveAndEnabled) {
			float translation = Input.GetAxis ("Vertical") * speed;
			float straffe = Input.GetAxis ("Horizontal") * speed;
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate (straffe, 0, translation);
		
			if (Input.GetButton ("Fire1")) {
				anim.SetBool ("isAttacking", true);
				FindObjectOfType<AudioManager> ().PlayDelayed ("Slash", 0.5f);
			} else {
				anim.SetBool ("isAttacking", false);
			}

			if (translation != 0) {
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isIdle", false);
			} else {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isIdle", true);
			}
		}

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void BeAttack (float damAttack)
	{
		health -= damAttack;
		healthbar.value = health / 100f;
		anim.SetBool ("isDamage", true);
		if (health <= 0) {
			anim.SetBool ("isDead", true);
			isAllive = false;
		}
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
