using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[System.Serializable]
	public class Sound
	{
		public string name;
		public AudioClip clip;
		public bool loop;

		// 2D or 3D sounds
		[Range (0f, 1f)]
		public float spatialBlend = 0.8f;

		[Range (0f, 1f)]
		public float volume = 0.8f;

		// Speed of sound
		[Range (0f, 2f)]
		public float pitch = 1f;

		[HideInInspector]
		public AudioSource source;
	}

	public Sound[] sounds;

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

			s.source.volume = s.volume;
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
