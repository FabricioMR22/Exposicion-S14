 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Animator animator;
	private Rigidbody2D rigidbody;
	public float force = 20f;
	public float speed = 5f;

	bool isPiso = true;
	public LayerMask maskPiso;
	public Transform checkpiso;
	private float radio = 0.1f;
	public bool doblesalto = false;
	private Vector2 posicionInicial;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D> ();
		posicionInicial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)){//caminar hacia la derecha
			animator.SetBool("isCaminar",true);
			transform.localRotation = Quaternion.Euler(0,0,0);
			transform.position += Vector3.right*speed*Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)){
			animator.SetBool("isCaminar",false);
		}

		if(Input.GetKey(KeyCode.LeftArrow)){//izquierda
			animator.SetBool("isCaminar",true);
			transform.localRotation = Quaternion.Euler(0,180,0);
			transform.position += Vector3.left*speed*Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			animator.SetBool("isCaminar",false);
		}



		if (Input.GetKeyDown(KeyCode.UpArrow)){
			//animator.SetBool("isSaltar",true);
			if (isPiso || doblesalto) {
				//transform.localRotation = Quaternion.Euler(0,0,0);
				rigidbody.velocity = new Vector2(rigidbody.velocity.x,force);
				rigidbody.AddForce(new Vector2(0,force));
				if (isPiso && !doblesalto) {
					doblesalto = true;
				}
			}

		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animator.SetBool ("isSaltar", false);
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			animator.SetBool("isAgachar",true);
			transform.localRotation = Quaternion.Euler(0,0,0);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
				animator.SetBool ("isAgachar", false);
		}
	}
	void FixedUpdate(){
	
		isPiso = Physics2D.OverlapCircle (checkpiso.position,radio,maskPiso);
		animator.SetBool ("isSaltar", !isPiso);
		if (isPiso) {
			doblesalto = false;
		}
		if (transform.position.y < -15) {
			transform.position = posicionInicial;
		}
	}

}