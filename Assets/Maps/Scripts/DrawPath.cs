using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;


public class DrawPath : MonoBehaviour {

	//private LineRenderer lineRenderer;
	private float counter;
	private float dist;
	private Vector3 prevPos;

	public Vector2 lineWidth=new Vector2(0.45f, 0.45f);
	public Material material;
	public string coordFileName;
	public float lineDrawSpeed=1f;

	//public List<LineRenderer> lineParts;
	internal List<GameObject> lineComparts;
	private bool isDrawingStarted = false;
	internal float startTimeWaiting=0f;
	private bool isDrawingInitiated=false;

	private List<Vector3> pA;
	private List<Vector3> pB;
	private float startTime;
	internal bool isAnimate=false;

	////// this part doesn't belong here, just remove it when you learned how to use othe scripts properties
	public Vector3 mapSize;
	public Material mapMaterial;
	// Map transform
	internal Vector3 mapPosition=new Vector3(0,0,0);
	internal Vector3 mapRotation=new Vector3(-13,0,0);
	// properties for projections from pixels to units
	internal Vector2 mapSizeInPixels;
	internal Vector2 pixels2Unit;
	internal Vector2 mapCenterInPixels;
	//////  end of removing in future
	private GameObject pathRendererObj;

	private int c=0;

	// Use this for initialization
//	void Start () {
//		StartDrawing ();
//
//
//
//	}
	void ComputePathPoints(Vector3[] pointsA, Vector3[] pointsB, bool isAnimate){
		//compute line components coordinates
		pA= new List<Vector3> ();
		pB= new List<Vector3> ();
		lineComparts = new List<GameObject> ();
		if (!isAnimate) {
			// it should be pointsB.Length because pointsA has an extra element
			for (int i=0; i<pointsB.Length; i++) {
				dist = Vector3.Distance (pointsA [i], pointsB [i]);
				//Debug.Log ("origin, X= " + pointsA [i].x.ToString () + ", Y= " + pointsA [i].y.ToString () + ", Z= " + pointsA [i].z.ToString ());
				//Debug.Log ("dest, X= " + pointsB [i].x.ToString () + ", Y= " + pointsB [i].y.ToString () + ", Z= " + pointsB [i].z.ToString ());

				pA.Add (pointsA [i]);
				pB.Add (pointsB [i]);
			}
		} else {// make them animate, add extra points inside
			for (int i=0; i<pointsB.Length; i++) {
				prevPos = pointsA [i];
				float dist = Vector3.Distance (pointsB [i], pointsA [i]);
				while (true) {
					pA.Add (prevPos);
					Vector3 offset = lineDrawSpeed * Vector3.Normalize (pointsB [i] - pointsA [i]);
					//Debug.Log ("offset, X= " + offset.x.ToString ()+", Y= "+offset.y.ToString ()+", Z= "+offset.z.ToString ());
					pB.Add (offset + prevPos);
					prevPos = pB [pB.Count - 1];
					float distCheck = Vector3.Distance (pB [pB.Count - 1], pointsB [i]);
					//Debug.Log ("distCheck= " + distCheck.ToString ());
					if (distCheck < lineDrawSpeed){
						pA.Add (prevPos);
						pB.Add (pointsB [i]);
					//	Debug.Log ("line ended# "+(i+1).ToString());
						break;
					}
				}
			}
			//Debug.Log ("pA.Count= " + pA.Count.ToString ());
			//StartDrawing();
		}
	}
	void FixedUpdate () {
		if (!isDrawingInitiated) {
			if (startTimeWaiting < Time.time) {
				isDrawingInitiated = true;
				startTime = Time.time;

			} else
				return;
		}
		
		if (isDrawingInitiated && isDrawingStarted) {
			if (c < pA.Count) {
				lineComparts.Add (new GameObject ("line"));
				lineComparts [lineComparts.Count - 1].transform.parent = pathRendererObj.transform;
				LineRenderer lr = lineComparts [lineComparts.Count - 1].AddComponent<LineRenderer> ();
				lr.SetWidth (lineWidth.x, lineWidth.y);
				lr.materials [0] = material;
				lr.sharedMaterial = material;
				lr.SetPosition (0, pA [c]);
				lr.SetPosition (1, pB [c]);
				c++;

			}
		}
	}
		public void StartDrawing(){
			isDrawingStarted = true;
		 pathRendererObj = GameObject.Find("PathRenderer");

		
		// some variables to convert from image coord to unity map coords
		mapSizeInPixels.x = mapMaterial.GetTexture (0).width;
		mapSizeInPixels.y = mapMaterial.GetTexture (0).height;
		pixels2Unit.x =   mapSize.x/mapSizeInPixels.x;
		pixels2Unit.y =   mapSize.z/mapSizeInPixels.y;
		mapCenterInPixels = new Vector2 (Mathf.Round(mapSizeInPixels.x / 2), Mathf.Round(mapSizeInPixels.y / 2));
		
		
		// get community coordinates
		var assetsPath = Application.streamingAssetsPath;
		assetsPath = Directory.GetParent (assetsPath).FullName;
		string coordDir = Path.Combine (assetsPath, @"Maps\Coord Files");
		string coordFilePath = Path.Combine (coordDir, coordFileName);
		
		//read  polygon
		List<Vector2> pathCoords = new List<Vector2> ();
		if (!LoadCoordinateFromTextFile.Load (coordFilePath, out pathCoords)) {
			return;
		}
		
		// compute community animation path
		Vector3[] pointsB = new Vector3[pathCoords.Count-1];
		Vector3[] pointsA = new Vector3[pathCoords.Count];
		float[] y2=new float[pathCoords.Count];
		for (int i=0; i<pathCoords.Count; i++) {
			pointsA[i].y=-(mapCenterInPixels.y-  pathCoords[i].y)*pixels2Unit.y * Mathf.Tan(mapRotation.x *Mathf.Deg2Rad)+mapSize.y;//+communities[i].transform.localScale.y/2f;
			pointsA[i].x=(pathCoords[i].x- mapCenterInPixels.x)*pixels2Unit.x ;
			pointsA[i].z=(pathCoords[i].y- mapCenterInPixels.y)*-pixels2Unit.y  ;
			//			pointsA[i]=pointsB[i];
			//			pointsA[i].y+=10;
		}
		for (int i=0; i<pathCoords.Count-1; i++) {
			pointsB [i] = pointsA [i + 1];
		}
		
		Debug.Log ("points b length= " + pointsB.Length.ToString());
		
		ComputePathPoints (pointsA, pointsB,isAnimate);

	}
	

}
