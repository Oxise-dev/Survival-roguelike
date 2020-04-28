using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource efxSourse;
	public AudioSource musicSourse;
	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.85f;

	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}
	
	public void PlaySingle(AudioClip clip)
	{
		efxSourse.clip = clip;
		efxSourse.Play();
	}

	public void RandomizeSfx(params AudioClip[] clips)
	{
		int RandomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		efxSourse.pitch = randomPitch;
		efxSourse.clip = clips[RandomIndex];
		efxSourse.Play();
	}
	void Update () {
		
	}
}
