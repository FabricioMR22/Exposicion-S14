using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemigoMovimiento : MonoBehaviour {
	public GameObject platform;
	public Transform[] points;
	public Transform currentPoint;
	public int pointPosition;
	private float velocity = 3f;
	// Use this for initialization
	void Start () {
		currentPoint = points[pointPosition];
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		platform.transform.position = Vector3.MoveTowards(platform.transform.position,currentPoint.position,Time.deltaTime*velocity);
		if (platform.transform.position == currentPoint.position){
			pointPosition = pointPosition + 1;
			if (pointPosition == points.Length){
				pointPosition = 0;
			}
			currentPoint = points[pointPosition];
		}
	}
}
