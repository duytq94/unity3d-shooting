using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightController : MonoBehaviour
{
	[Range (0f, 100f)]
	public float health = 100f;
	public float originSpeed = 5f;
	public float maxSpeed = 15f;

	public VirtualJoystick joystick;

	private GameObject knightCamera;
	private Slider healthbar;
	private Animator animator;
	private bool isAllive = true;
	private float currentSpeed;

	private Button attackButton;

	// Use this for initialization
	void Start ()
	{
		healthbar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Slider> ();
		knightCamera = GameObject.FindGameObjectWithTag ("KnightCamera");

		healthbar.value = health / 100f;
		animator = GetComponent<Animator> ();
		currentSpeed = originSpeed;

		attackButton = GameObject.FindGameObjectWithTag ("AttackButton").GetComponent<Button> ();
		attackButton.onClick.AddListener (ProcessAttack);
	}

	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			return;
		}

		if (knightCamera.GetComponent<Camera> ().isActiveAndEnabled) {

			float translation = joystick.Vertical () * currentSpeed;
			float straffe = joystick.Horizontal () * currentSpeed;

			translation *= Time.deltaTime;
			straffe *= Time.deltaTime;

			transform.Translate (straffe, 0, translation);

			if (translation != 0) {
				animator.SetBool ("isWalking", true);
				animator.SetBool ("isIdle", false);
			} else {
				animator.SetBool ("isWalking", false);
				animator.SetBool ("isIdle", true);
			}
		}
	}

	public void ProcessAttack ()
	{
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			animator.SetBool ("isAttacking", true);
			StartCoroutine (SetFalseAnimation ("isAttacking", 0.8f));
			FindObjectOfType<AudioManager> ().PlayDelayed ("Slash", 0.2f);
		}
	}

	public void OnBtnFastHold ()
	{
		currentSpeed = maxSpeed;
		print ("hold");
	}

	public void onBtnFastRelease ()
	{
		currentSpeed = originSpeed;
		print ("release");
	}

	IEnumerator SetFalseAnimation (string name, float time)
	{
		yield return new WaitForSeconds (time);
		animator.SetBool (name, false);
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
