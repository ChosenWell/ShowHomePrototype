using UnityEngine;
using System.Collections;

public class DelayedAppear : MonoBehaviour {


	public float timer = 2f;

	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false; 
	
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < Time.time) {
			gameObject.renderer.enabled = true;
		}
	}
}
