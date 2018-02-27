using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : util.Singleton<SoundManager> {

	[SerializeField] AudioClip click;
	AudioSource audioSource;

	private void Start()
	{
		Instance.audioSource = GetComponent<AudioSource>();
	}

	static public void Click()
	{
		Instance.audioSource.PlayOneShot(Instance.click,0.35f);
	}


}
