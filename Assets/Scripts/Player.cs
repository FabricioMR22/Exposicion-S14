using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Animator animator;
	private Rigidbody2D rigidbody;
	public float force = 20f; //fuerza de salto
	public float forc = 0f; //fuerza agacharse
	public float velocidad = 7f;
	
	bool isPiso = true;
	public LayerMask maskPiso;
	public Transform checkpiso;
	private float radio = 0.1f;

	public bool doblesalto = false;
	public bool agacharse = false;

	private Vector2 posicionInicial;//si se cae el player

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		posicionInicial = transform.position;//posicion de inicio de juego
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)){// Caminar hacia la derecha
			animator.SetBool("isCaminar",true);
			transform.localRotation = Quaternion.Euler(0,0,0);
			transform.position += Vector3.right*velocidad*Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)){// parar de caminar a la derecha
			animator.SetBool("isCaminar",false);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){// Caminar hacia la izquierda
			animator.SetBool("isCaminar",true);
			transform.localRotation = Quaternion.Euler(0,180,0);
			transform.position += Vector3.left*velocidad*Time.deltaTime;
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)){// parar de caminar a la izquierda
			animator.SetBool("isCaminar",false);
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)){// saltar
			//animator.SetBool("isSaltar",true);
			if (isPiso || doblesalto){
				//transform.localRotation = Quaternion.Euler(0,0,0);
				rigidbody.velocity = new Vector2(rigidbody.velocity.x,force);
				rigidbody.AddForce(new Vector2(0,force));
				if (isPiso && !doblesalto){
					doblesalto = true;
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animator.SetBool ("isSaltar", false);
		}


		if (Input.GetKey(KeyCode.DownArrow)){
			animator.SetBool("isAgachar",true);
			if (isPiso || agacharse){
			//transform.localRotation = Quaternion.Euler(0,0,0);
				rigidbody.velocity = new Vector2(rigidbody.velocity.x,forc);
				if (isPiso && !agacharse){
					agacharse = true;
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
				animator.SetBool ("isAgachar", false);
		}
	}

	void FixedUpdate(){
		isPiso = Physics2D.OverlapCircle(checkpiso.position,radio,maskPiso);
		animator.SetBool("isSaltar", !isPiso);
		animator.SetBool("isAgachar", !isPiso);
		if (isPiso){
			doblesalto = false;
			agacharse = false;
		}

		if (transform.position.y< -15) { //posicion de arriba/abajo (y) del player es mayor regresa al inicio automatico
			transform.position = posicionInicial;
		}
	}

	void OnTriggerEnter2D(Collider2D fantasma){
		Debug.Log("Hola");
		if(fantasma.gameObject.tag == "Enemigo"){
			transform.position = posicionInicial;
		}
	}


}