using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotOn : MonoBehaviour {

	public bool OnGround;

	// Use this for initialization
	void Start () {
		OnGround = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay2D(Collider2D col){

		if (col.gameObject == null) {  
			OnGround = false;
			//Debug.Log ("Grounded");

		}
	}
	/*
	void OnTriggerExit2D(Collider2D col){

		if (col.gameObject.layer == 8 || col.gameObject.layer == 11) {  
			OnGround = false;
			//Debug.Log ("NotGrounded");

		}


	}
	*/
}
