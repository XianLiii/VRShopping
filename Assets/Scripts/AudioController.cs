using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	//public AudioClip nature;
	public AudioSource source; 
	public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {

	}

	public void PlaySound(int clip)
	{
		source.clip = audioClip [clip];
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
