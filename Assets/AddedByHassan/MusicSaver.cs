using UnityEngine;
using System.Collections;
public class MusicSaver : MonoBehaviour {
	public AudioClip myClip;
	public static bool MusicFlag=true;
	//public float Level=1f;
	//public bool MusicButtonFlag=true;
	void Start()
	{
		GetComponent<AudioSource>().PlayOneShot(myClip);

		if (MusicFlag) {
			MusicFlag = false;
			DontDestroyOnLoad (gameObject);
		} else
			Destroy (gameObject);


	}
	/*
 void Update()
	{
		if (Application.loadedLevelName == "Options")
		GetComponent<AudioSource>().volume = Level;
	}
	void Start()
	{
		if (myClip)
			DestroyImmediate (gameObject);
		else {
			DontDestroyOnLoad (this.gameObject);
			myClip = this;
			
		}
	}

//
*/
}