using UnityEngine;
using System.Collections;

public class StopSmoke : MonoBehaviour {
	public float timer = 1f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timer < Time.time)
		{
			GetComponent("EllipsoidParticleEmitter").particleEmitter.maxEmission=0;
			//Debug.Log ("3 Seconds Reached");
		}
	}
}
