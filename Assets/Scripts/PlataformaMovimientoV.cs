using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovimientoV : MonoBehaviour {

	public GameObject platform;
	public Transform[] points; //puntos inicio y fin
	public Transform currentPoint; //punto actual del movimiento
	public int pointPosition;//que posicion esta
	private float velocity= 3f;

	// Use this for initialization
	void Start () {
		currentPoint = points [pointPosition];
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){ //repetir constantemente
		platform.transform.position = Vector3.MoveTowards(platform.transform.position,currentPoint.position,Time.deltaTime*velocity);
		if (platform.transform.position==currentPoint.position){
			pointPosition = pointPosition + 1;
			if(pointPosition == points.Length){
				pointPosition = 0;
			}
			currentPoint = points[pointPosition];
		}
	}
}