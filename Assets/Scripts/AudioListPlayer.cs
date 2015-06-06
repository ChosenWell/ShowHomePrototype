using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioListPlayer : MonoBehaviour {
//	public float bpm = 140.0F;
//	public int numBeatsPerSegment = 16;
	public AudioClip[] clips ;//= new AudioClip[2];
//	private double nextEventTime;
//	private int flip = 0;
	private AudioSource[] audioSources = new AudioSource[2];
//	private bool running = false;

	public float timeOffset=5.0f;


	IEnumerator Start() {

		// initialize audio sources
		int i = 0;
		while (i < clips.Length) 
		{
			GameObject child = new GameObject("Player");
			child.transform.parent = gameObject.transform;
			audioSources[i] = child.AddComponent<AudioSource>();
			audioSources[i].clip = clips[i];
			i++;
		}

		if (clips.Length == 1) {
			yield return new WaitForSeconds (timeOffset);
			audioSources [0].Play ();
		} else {
			for (i=0; i<clips.Length-1; i++) {
				if (i == 0) {
					yield return new WaitForSeconds (timeOffset);
					audioSources [0].Play ();
				}

				yield return new WaitForSeconds (audioSources [i].clip.length);
				//audio.clip = otherClip;
				audioSources [i + 1].Play ();
			}
		}

//		//AudioSource audio = GetComponent<AudioSource>();
//		yield return new WaitForSeconds(timeOffset);
//		audioSources[0].Play ();
//	//	audio.Play();
//		yield return new WaitForSeconds(audioSources[0].clip.length);
//		//audio.clip = otherClip;
//		audioSources[1].Play();
//		}
	}




//	void Start() 
//	{
//
//		int i = 0;
//		while (i < 2) {
//			GameObject child = new GameObject("Player");
//			child.transform.parent = gameObject.transform;
//			audioSources[i] = child.AddComponent<AudioSource>();
//			i++;
//		}
//		nextEventTime = AudioSettings.dspTime + 2.0F;
//		running = true;
//	}
//	void Update() {
//		if (!running)
//			return;
//		
//		double time = AudioSettings.dspTime;
//		if (time + 1.0F > nextEventTime) {
//			audioSources[flip].clip = clips[flip];
//			audioSources[flip].PlayScheduled(nextEventTime);
//			Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);
//			nextEventTime += 60.0F / bpm * numBeatsPerSegment;
//			flip = 1 - flip;
//
//
//		}
//	}
}