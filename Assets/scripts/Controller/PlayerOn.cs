using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOn : MonoBehaviour {

    public bool OnLift;

    public GameObject Lift;

	// Use this for initialization
	void Start () {
        OnLift = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Lift")
        {
            Lift = collision.gameObject;
            Debug.Log("LiftON");
            OnLift = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Lift")
        {
            Lift = null;
            Debug.Log("LiftOFF");
            OnLift = false;
        }
    }
}
