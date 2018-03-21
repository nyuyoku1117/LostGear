﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour {

	//倒れているかフラグ
	public bool BridgeActionFlag;



	// Use this for initialization
	void Start () {
		BridgeActionFlag = false;
	}
	
	// Update is called once per frame
	public void BridgeAction() {

		iTween.RotateBy (this.gameObject, iTween.Hash("z",-0.25,"time",3));
		Debug.Log ("Bridge_Action!");
		BridgeActionFlag = true;

	}
}
