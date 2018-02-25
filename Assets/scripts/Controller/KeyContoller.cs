using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyContoller : MonoBehaviour {

    public GameObject gate;

    public bool open;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void gateOpen() {
        gate.SetActive(false);
        open = true;
    }

    public void gateClose()
    {
        gate.SetActive(true);
        open = false;
    }
}
