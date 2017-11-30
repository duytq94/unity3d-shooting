using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	public void PlayGame ()
	{
		SceneManager.LoadScene ("Main");
	}

	public void ExitApp ()
	{
		Application.Quit ();
	}
		
}
