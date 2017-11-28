using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{

	public Camera[] cams;

	public void CamPlayerMove ()
	{
		cams [0].enabled = true;
		cams [1].enabled = false;
	}

	public void CamFlyMove ()
	{
		cams [0].enabled = false;
		cams [1].enabled = true;
	}
}
