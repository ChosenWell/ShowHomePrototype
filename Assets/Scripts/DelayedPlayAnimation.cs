using UnityEngine;
using System.Collections;

public class DelayedPlayAnimation : MonoBehaviour {

	public float timer = 1f;
	
	// Use this for initialization
	void Start ()
	{
		//gameObject.active = false;
		//GetComponent(MeshRenderer).enabled = false;
		GetComponent<Animator> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timer < Time.time) {
			//gameObject.active = true;
			//GetComponent(MeshRenderer).enabled = true;
			GetComponent<Animator> ().enabled = true;
			//Debug.Log ("3 Seconds Reached");
		}
	}
}
