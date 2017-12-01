using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;

	[Range (0f, 1f)]
	public float volume = 1f;

	public static AudioManager instance;

	// Use this for initialization
	void Awake ()
	{
//		if (instance == null) {
//			instance = this;	
//			print ("null");
//		} else {
//			Destroy (gameObject);
//			print ("not null");
//			return;
//		}
//		DontDestroyOnLoad (gameObject);


		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.spatialBlend = s.spatialBlend;
		}
	}

	void Start ()
	{
		Play ("DesertWind");
	}

	public void Play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Play ();
	}

	public void Pause (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Pause ();
	}
}
