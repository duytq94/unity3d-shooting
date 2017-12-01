using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public string name;
	public AudioClip clip;
	public bool loop;

	// 2D or 3D sounds
	[Range (0f, 1f)]
	public float spatialBlend = 0.8f;

	// Speed of sound
	[Range (0f, 2f)]
	public float pitch = 1f;

	[HideInInspector]
	public AudioSource source;
}
