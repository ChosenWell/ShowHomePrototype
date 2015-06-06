using UnityEngine;
using System.Collections;

public class BlinkObject : MonoBehaviour {


	public float offsetTime = 5f;
	public float duration=2f;
	private bool isStarted=false;
	public int count =3;
	private int c=0;
	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (offsetTime < Time.time) {
			renderer.enabled = true;
			if (c < count) {
				if (Time.fixedTime % .5 < .2) {
					if (renderer.isVisible)
						c++;
					renderer.enabled = false;

				} else {
					renderer.enabled = true;
				}

			}

		}
	}
//	IEnumerator Blink()
//	{
//		renderer.enabled = false;
//		yield return new WaitForSeconds(0.2f);
//		renderer.enabled = true;
//	}
//
//
//	IEnumerator  Blink(float waitTime ) {
//
//		var endTime=Time.time + waitTime;
//		while(Time.time<waitTime){
//			renderer.enabled = false;
//			yield return new WaitForSeconds(0.2f);
//			renderer.enabled = true;
//			yield return new WaitForSeconds(0.2f);
//		}
//	}



}
