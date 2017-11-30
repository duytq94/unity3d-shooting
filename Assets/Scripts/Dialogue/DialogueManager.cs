using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Animator animator;

	public void StartDialogue ()
	{
		animator.SetBool ("IsOpen", true);
	}

	public void CloseDialogue ()
	{
		animator.SetBool ("IsOpen", false);
	}
}
