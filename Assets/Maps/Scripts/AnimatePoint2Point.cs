using UnityEngine;
using System.Collections;

public class AnimatePoint2Point : MonoBehaviour {

	public Vector3 pointA;
	public Vector3 pointB;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;
	private bool isAnimationStarted = false;
	internal float startTimeWaiting=0f;
	internal bool isDisplayedBeforeAnim=true;
	private bool isAnimationInitiated=false;

	void Start(){
		if (!isDisplayedBeforeAnim) {
//			Debug.Log ("disabling object! " );
			SetActiveRenderer(gameObject, false);
		}


	}
	public AnimatePoint2Point(Vector3 pointA, Vector3 pointB, float speed){
		this.pointA = pointA;
		this.pointB = pointB;
		this.speed = speed;

	}

	void Update() {
		if (!isAnimationInitiated) {
			if (startTimeWaiting < Time.time) {
				isAnimationInitiated = true;
				startTime = Time.time;
				if (!isDisplayedBeforeAnim)
					SetActiveRenderer(gameObject, true);
			} else
				return;
		}

//		Debug.Log ("isAnimationInitiated= "+isAnimationInitiated.ToString());
		if (isAnimationInitiated && isAnimationStarted) {
		
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
//			Debug.Log ("frac journey= " + fracJourney.ToString());
			gameObject.transform.position = Vector3.Lerp (pointA, pointB, fracJourney);

		}
	}
	public void StartAnimation(){

		isAnimationStarted = true;
		//startTime = Time.time;
		//Debug.Log ("startTime="+startTime.ToString());
		journeyLength = Vector3.Distance (pointA, pointB);
		//Debug.Log ("journeyLength="+journeyLength.ToString());
	}
	private void SetActiveRenderer(GameObject go, bool isActive){
		var renderers = go.GetComponentsInChildren <Renderer>();
		foreach (Renderer r in  renderers) {
			r.enabled = isActive;
		}
	}

}
