using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateContoller : MonoBehaviour {

	private GameObject KOBJ;
	public Rigidbody2D rigidbody2D;
	private Coroutine coroutine;
	public float GateUpTime = 3.0f;
	public float GateUpSpeed = 1.0f;
	private bool GateUpFlag = false;
	private bool i=false;

	// Use this for initialization
	void Start () {

		KOBJ = GameObject.Find ("player");
		rigidbody2D = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update ()
	{

		PlayerControler PC = KOBJ.GetComponent<PlayerControler> ();

		if (PC.isGotten == true) {
			if (i == false) {
				GateUpFlag = true;
				i = true;
			}
			if (GateUpFlag == true) {
				rigidbody2D.velocity = new Vector2 (0, GateUpSpeed);

				if (coroutine == null) {
					coroutine = StartCoroutine ("DelayMethod");
				}
					
			} 
				
		}
	}

	private IEnumerator DelayMethod ()
	{
		yield return new WaitForSeconds (GateUpTime);
		GateUpFlag = false;
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
}
