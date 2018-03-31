using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashigoController : MonoBehaviour {
	private Rigidbody2D rigidbody2D;

	public bool Xmove = false;
	public bool Ymove = false;

	public float XmoveRange;
	public float YmoveRange;
	public float XmoveSpeed;
	public float YmoveSpeed;

	private float movetime;
	private float movelimit;

	private bool moveflag = false;

	// Use this for initialization
	void Start () {
		movetime = 0;
		movelimit = 0;
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (Xmove && Ymove) {


		} else if (Xmove) {

			movelimit = XmoveRange / XmoveSpeed;
			if (movetime <= movelimit && movelimit != 0) {
				
				movetime += Time.deltaTime;
				rigidbody2D.velocity = new Vector2 (XmoveSpeed, 0);

			} else {

				rigidbody2D.velocity = Vector2.zero;
				Xmove = false;

			}

		} else if (Ymove) {



		} else {


		}

	}
}
