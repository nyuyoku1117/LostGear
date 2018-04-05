using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour {

    public GameObject otherDoor;

    private GameObject player;

    public void Moving()
    {
        player = GameObject.Find("player");
        player.transform.position = otherDoor.transform.position;
    }
}
