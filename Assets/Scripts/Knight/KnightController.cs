using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
	[Range (0f, 100f)]
	public float health = 100f;
	public float speed = 1f;

	private GameObject knightCamera;
	private Slider healthbar;
	private Animator animator;
	private bool isAllive = true;

	// Use this for initialization
	void Start ()
	{
		healthbar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Slider> ();
		knightCamera = GameObject.FindGameObjectWithTag ("KnightCamera");

		healthbar.value = health / 100f;
		animator = GetComponent<Animator> ();
		Cursor.lockState = CursorLockMode.Locked;
		speed = speed / 20f;
	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			return;
		}

		if (knightCamera.GetComponent<Camera> ().isActiveAndEnabled) {
			float translation = Input.GetAxis ("Vertical") * speed;
			float straffe = Input.GetAxis ("Horizontal") * speed;
			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate (straffe, 0, translation);
		
			if (Input.GetButton ("Fire1") && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack1")) {
				animator.SetBool ("isAttacking", true);
				FindObjectOfType<AudioManager> ().PlayDelayed ("Slash", 0.3f);
			} else {
				animator.SetBool ("isAttacking", false);
			}

			if (translation != 0) {
				animator.SetBool ("isWalking", true);
				animator.SetBool ("isIdle", false);
			} else {
				animator.SetBool ("isWalking", false);
				animator.SetBool ("isIdle", true);
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
		animator.SetBool ("isDamage", true);
		if (health <= 0) {
			animator.SetBool ("isDead", true);
			isAllive = false;
		}
	}

	public void ExitAttack ()
	{
		animator.SetBool ("isDamage", false);
	}

	public bool GetIsAllive ()
	{
		return isAllive;
	}
}
