using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibleController : MonoBehaviour {

    private bool inFlag;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        var parent = this.gameObject;
        
        if (inFlag == true)
        {
            foreach (Transform child in parent.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            foreach (Transform child in parent.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            inFlag = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            inFlag = false;
        }
    }
}
