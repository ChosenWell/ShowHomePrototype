using UnityEngine;
using System.Collections;

public class DelayedActive: MonoBehaviour {
	
	public GameObject thisGameObject;
	public float timer = 2f;
	//public float rrefew;
	
	// Use this for initialization
	void Start () {
		thisGameObject.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < Time.time) {
			thisGameObject.SetActive(true);
		}
	}
}
