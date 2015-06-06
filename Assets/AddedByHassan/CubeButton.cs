using UnityEngine;
using System.Collections;

public class CubeButton : MonoBehaviour 
{
//	public GameObject gameObject;
	
	public Texture2D FadeImage;
	//Rect button=(new Rect(225,15,100,40));
	//	public string Label="Load Level";
	float FadeSpeed=0.5f;
	
	float alphaLevel=1f;//(between 0 and 1)
	int Depth = -1000;
	int Direction=-1;
	bool myFlag=true;
	bool myFlag1=true;
	public int ToLoad=0;
	void OnMouseOver()
	{
		renderer.material.color = Color.yellow;
		if (myFlag == true) {
			transform.localScale += new Vector3 (0.5f, 0.5f, 0);
			myFlag=false;
			myFlag1=true;
		}
	}
	void Awake()
	{
	
		renderer.material.color = Color.cyan; 
	}
	void OnMouseExit()
	{
		renderer.material.color = Color.cyan; 
		if (myFlag1 == true) 
		{
			transform.localScale -= new Vector3 (0.5f, 0.5f, 0);
			myFlag = true;
			myFlag1 = false;
		}

	}
	void OnMouseDown()
	{
		if(alphaLevel<0.1)
		Direction = 1;
	}

	
	void OnGUI()
	{
			//renderer.material.color = Color.green;
		//			Application.LoadLevel (ToLoad);
		alphaLevel = alphaLevel + (Direction * FadeSpeed * Time.deltaTime);//The operation is conducted in seconds unit
		alphaLevel = Mathf.Clamp01 (alphaLevel);//Clamp the alphaLevel between 0 and 1 (Needed by onGUI())
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alphaLevel);
		GUI.depth = Depth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), FadeImage);
		if (alphaLevel == 1 && Direction == 1)
			Application.LoadLevel (ToLoad);
	}
	
}