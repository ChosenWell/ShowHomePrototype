using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPathList : MonoBehaviour {

	public Vector2 lineWidth=new Vector2(0.45f, 0.45f);
	public Material material;
	//public string coordFileName;
	public float lineDrawSpeed=1f;
	////// this part doesn't belong here, just remove it when you learned how to use othe scripts properties
	public Vector3 mapSize;
	public Material mapMaterial;

	
	public List<float> drawingsTimeOffsets;
	public List<string> pathFileNames;

	// Use this for initialization
	void Start () {

		List<DrawPath> dps = new List<DrawPath> ();
		for (int i=0;i<drawingsTimeOffsets.Count;i++){
		DrawPath dp = gameObject.AddComponent <DrawPath> ();
			dp.coordFileName=pathFileNames[i];
			dp.startTimeWaiting=drawingsTimeOffsets[i];
			dp.lineWidth=lineWidth;
			dp.material=material;
			dp.lineDrawSpeed=lineDrawSpeed;
			dp.mapSize=mapSize;
			dp.mapMaterial=mapMaterial;
			dp.isAnimate=true;
			dp.StartDrawing();
			dps.Add(dp);

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
