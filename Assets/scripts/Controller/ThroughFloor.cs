﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughFloor : MonoBehaviour {
	public PlayerControler PC;
    public BoxCollider2D ChildBoxCollider;

	public bool Xmove = false;
	public bool Ymove = false;

	public float XmoveRange;
	public float YmoveRange;

	public float XmoveSpeed;
	public float YmoveSpeed;

	private float MoveTime;

    // Use this for initialization
    void Start () {
        ChildBoxCollider = this.GetComponent<BoxCollider2D>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (PC.transform.position.y >= this.transform.position.y - 0.2f && PC.transform.position.y <= this.transform.position.y + 0.1f) {
            ChildBoxCollider.enabled = true;
		} else {
            ChildBoxCollider.enabled = false;
		}


	}
}
