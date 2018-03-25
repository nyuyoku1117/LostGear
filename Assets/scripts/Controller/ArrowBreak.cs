using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBreak : MonoBehaviour {

	private float now;


	// Use this for initialization
	void Start () {

		now = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (now > 4f) {

			Destroy (this.gameObject);
			return;

		}

		else if (now <= 4f) {
			now += Time.deltaTime;
		}
	}
}
