using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGetAttack : MonoBehaviour
{

	public Slider healthBar;
	private float health = 100f;

	// Use this for initialization
	void Start ()
	{
		healthBar.value = health / 100f;
	}

	public void beAttack (float value)
	{
		health -= value;
		if (health / 100f >= 0) {
			healthBar.value = health / 100f;
		}
	}

}
