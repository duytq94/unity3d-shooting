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
	public Button attackButton;

	private GameObject knightCamera;
	private Slider healthbar;
	private Animator animator;
	private bool isAllive = true;
	private float currentSpeed;

	private Image bloodyScreen;
	private VirtualJoystick joystick;

	void Start ()
	{
		healthbar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Slider> ();
		knightCamera = GameObject.FindGameObjectWithTag ("KnightCamera");

		healthbar.value = health / 100f;
		animator = GetComponent<Animator> ();
		currentSpeed = originSpeed;

		attackButton.onClick.AddListener (ProcessAttack);

		bloodyScreen = GameObject.FindGameObjectWithTag ("BloodyScreen").GetComponent<Image> ();
		joystick = GameObject.FindGameObjectWithTag ("Joystick").GetComponent<VirtualJoystick> ();
	}
		
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
	}

	public void OnBtnFastRelease ()
	{
		currentSpeed = originSpeed;
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

		bloodyScreen.enabled = true;
		StartCoroutine (DisableBloodyScreen (2f));

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

	IEnumerator DisableBloodyScreen (float time)
	{
		yield return new WaitForSeconds (time);
		bloodyScreen.enabled = false;
	}
}
