using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp_nightController : MonoBehaviour {

	public int currentPointa;
	float now = 0;
	private bool moveflag = true;

	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	public GameObject item5;


	// Use this for initialization
	void Start () {

		currentPointa = 1;




	}
	
	// Update is called once per frame
	void Update () {
		if (moveflag == false) {

			now += Time.deltaTime;

		}

		if (now > 1.5) {
			moveflag = true;
			now = 0;
		}
		
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			
			if (currentPointa != 5) {
				if (moveflag) {
					currentPointa += 1;
					Debug.Log (currentPointa);
					iTween.MoveBy (this.gameObject, iTween.Hash ("y", -50));
					moveflag = false;
				}
			}
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
				
			if (currentPointa != 1) {
				if (moveflag) {
					currentPointa -= 1;
					Debug.Log (currentPointa);
					iTween.MoveBy (this.gameObject, iTween.Hash ("y", 50f));
					moveflag = false;
				}
			}
		}
				
		if (Input.GetKeyDown (KeyCode.Z)) {
			

			if (currentPointa == 1) {
				if (moveflag) {
					currentPointa = 11;
					Debug.Log (currentPointa);
					iTween.MoveBy (item1, iTween.Hash ("x", -455f));
					iTween.MoveBy (item2, iTween.Hash ("x", -455f));
					iTween.MoveBy (item3, iTween.Hash ("x", -455f));
					iTween.MoveBy (item4, iTween.Hash ("x", -455f));
					iTween.MoveBy (item5, iTween.Hash ("x", -455f));
					moveflag = false;
				}
			} else if (currentPointa == 2) {


			} else if (currentPointa == 3) {


			} else if (currentPointa == 4) {


			} else if (currentPointa == 5) {

			}
		}


			 
		if (currentPointa == 11) {

			if (Input.GetKeyDown (KeyCode.Space)) {

				if (moveflag) {
					currentPointa = 1;
					Debug.Log (currentPointa);
					iTween.MoveBy (item1, iTween.Hash ("x", 455f));
					iTween.MoveBy (item2, iTween.Hash ("x", 455f));
					iTween.MoveBy (item3, iTween.Hash ("x", 455f));
					iTween.MoveBy (item4, iTween.Hash ("x", 455f));
					iTween.MoveBy (item5, iTween.Hash ("x", 455f));
					moveflag = false;
				}

			}

		}

	}

}
