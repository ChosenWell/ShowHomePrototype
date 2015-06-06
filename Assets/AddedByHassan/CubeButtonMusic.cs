using UnityEngine;
using System.Collections;

public class CubeButtonMusic : MonoBehaviour 
{
	bool myFlag=true;
	bool myFlag1=true;
	bool myFlag2=false;
	public float MLevel=1;
	void OnMouseOver()
	{
		myFlag2 = true;
				renderer.material.color = Color.green;
		if (myFlag == true) {
			transform.localScale += new Vector3 (0.2f, 0.2f, 0);
			myFlag=false;
			myFlag1=true;
		}
	}
//	void Awake()
//	{
	//	GetComponent<MusicSaver>().enabled = false;

//		renderer.material.color = new Color(0.8f, 0.8f, 0.0f, 1.0f); 
//	}
	void OnMouseExit()
	{
		myFlag2 = false;

//		renderer.material.color = new Color(0.8f, 0.8f, 0.0f, 1.0f); 
		if (myFlag1 == true) 
		{
			transform.localScale -= new Vector3 (0.2f, 0.2f, 0);
			myFlag = true;
			myFlag1 = false;
		}
		
	}
	void OnMouseDown()
	{
		if (GameObject.Find ("MusicObject") != null)
		GameObject.Find ("MusicObject").GetComponent<AudioSource>().volume = MLevel;
	}
	
	void Update()
	{
		if (GameObject.Find ("MusicObject") != null)
		if (GameObject.Find ("MusicObject").GetComponent<AudioSource> ().volume < MLevel)
				renderer.material.color = new Color (0.8f, 0.8f, 0.0f, 1.0f);
		else if(GameObject.Find ("MusicObject").GetComponent<AudioSource> ().volume >= MLevel)
			if (myFlag2==false)
			renderer.material.color = Color.blue;

	}
	
}