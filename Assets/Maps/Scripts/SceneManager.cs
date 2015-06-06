using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;

public class SceneManager : MonoBehaviour {


	// public properties
	public GameObject map;
	public Material mapMaterial;
	public GameObject cam;
	public Vector3 mapSize;
	public string inputCoordinationFile;
	public string pathCoordnationFile;
	public GameObject theHouse;
	public List<GameObject> communities;
	public List<float> communityAnimTimeOffsets;

	// properties for projections from pixels to units
	internal Vector2 mapSizeInPixels;
	internal Vector2 pixels2Unit;
	internal Vector2 mapCenterInPixels;

	private bool isAnimationStarted = false;
	internal float startTimeWaiting=0f;



	// Map transform
	internal Vector3 mapPosition=new Vector3(0,0,0);
	internal Vector3 mapRotation=new Vector3(-13,0,0);
	internal Vector3 mapScale;

	// camera transform
	internal Vector3 camPosition;
	internal Vector3 camRotation=new Vector3(21,0,0);
	internal Vector3 camScale=new Vector3(0,1,1);

	//objects pos
	internal List<Vector2>  communityCoords;

	internal float offsetY=30;

	// Use this for initialization
	void Start () {

		// Map transform
		mapScale=new Vector3(-mapSize.x,3,-mapSize.z);
		map.transform.position = mapPosition;
		map.transform.rotation =Quaternion.Euler( mapRotation);
		map.transform.localScale = mapScale;

		// camera transform
		camPosition=new Vector3(0,50,-(mapSize.z+20));
		cam.transform.position = camPosition;
		cam.transform.eulerAngles = camRotation;
		cam.transform.localScale = camScale;

		// get community coordinates
		var assetsPath = Application.streamingAssetsPath;
		assetsPath = Directory.GetParent (assetsPath).FullName;
		string coordDir = Path.Combine (assetsPath, @"Maps\Coord Files");
		string coordFilePath = Path.Combine (coordDir, inputCoordinationFile);
		Debug.Log ("file path= " + coordFilePath);

		communityCoords = new List<Vector2> ();
		if (!LoadCoordinateFromTextFile.Load (coordFilePath, out communityCoords)) {
			return;
		}

		//since the first coordinate is the coord of house, seprate it from community
		Vector2 houseCoord = communityCoords [0];
		communityCoords= communityCoords.GetRange (1, communityCoords.Count - 1);

		//apply rotation  to the house
		GameObject houseGO = GetChildGameObject (theHouse, "The House");
		houseGO.transform.rotation = Quaternion.Euler (mapRotation);

		
		//apply rotation to community objects
		foreach (var co in communities) {
			co.transform.localRotation = Quaternion.Euler (mapRotation);
		}

		// some variables to convert from image coord to unity map coords
		mapSizeInPixels.x = mapMaterial.GetTexture (0).width;
		mapSizeInPixels.y = mapMaterial.GetTexture (0).height;
		pixels2Unit.x =   mapSize.x/mapSizeInPixels.x;
		pixels2Unit.y =   mapSize.z/mapSizeInPixels.y;
		mapCenterInPixels = new Vector2 (Mathf.Round(mapSizeInPixels.x / 2), Mathf.Round(mapSizeInPixels.y / 2));

		//apply coordinate to the house
		Vector3 pos = new Vector3 ();
		pos.y=-(mapCenterInPixels.y-  houseCoord.y)*pixels2Unit.x * Mathf.Tan(mapRotation.x *Mathf.Deg2Rad)+mapSize.y+theHouse.transform.localScale.y/2f;
		pos.x=(houseCoord.x- mapCenterInPixels.x)*pixels2Unit.x  ;
		pos.z=(houseCoord.y- mapCenterInPixels.y)*-pixels2Unit.y  ;
		houseGO.transform.position = pos;

		

		// compute community animation path
		Vector3[] pointsB = new Vector3[communityCoords.Count];
		Vector3[] pointsA = new Vector3[communityCoords.Count];
		float[] y2=new float[communityCoords.Count];
		for (int i=0; i<communityCoords.Count; i++) {
			pointsB[i].y=-(mapCenterInPixels.y-  communityCoords[i].y)*pixels2Unit.y * Mathf.Tan(mapRotation.x *Mathf.Deg2Rad)+mapSize.y+communities[i].transform.localScale.y/2f;
			pointsB[i].x=(communityCoords[i].x- mapCenterInPixels.x)*pixels2Unit.x ;
			pointsB[i].z=(communityCoords[i].y- mapCenterInPixels.y)*-pixels2Unit.y  ;
			pointsA[i]=pointsB[i];
			pointsA[i].y+=offsetY;
		}

		//assign animation to the house
		AnimatePoint2Point ap2p = houseGO.AddComponent <AnimatePoint2Point>() as AnimatePoint2Point;
		ap2p.pointB=houseGO.transform.position;
		ap2p.pointA=ap2p.pointB;
		ap2p.pointA.Set (ap2p.pointA.x, ap2p.pointA.y-10f, ap2p.pointA.z);
		ap2p.speed=10f;
		ap2p.isDisplayedBeforeAnim=false;
		ap2p.startTimeWaiting=0.0f;
		ap2p.StartAnimation();

		
		
		//assign animation to community objects
		for (int i=0; i<communityCoords.Count; i++) {
			communities[i].transform.position=pointsA[i];
			ap2p = communities[i].AddComponent <AnimatePoint2Point>() as AnimatePoint2Point;
			ap2p.pointA=pointsA[i];
			ap2p.pointB=pointsB[i];
			ap2p.speed=20f;
			ap2p.isDisplayedBeforeAnim=false;
			ap2p.startTimeWaiting=communityAnimTimeOffsets[i];
			ap2p.StartAnimation();
		}






//		GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
//		cylinder.transform.position = new Vector3(-2, 1, 0);
//		cylinder.transform.localScale = new Vector3(5, 5, 5);
//		cylinder.transform.rotation= Quaternion.Euler(
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	static public GameObject GetChildGameObject(GameObject fromGameObject, string withName) {
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}
}
