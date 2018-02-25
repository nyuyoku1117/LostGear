using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public LiftController[] Lift;

    private int LiftNum;
    // Use this for initialization
    void Start () {
        LiftNum = Lift.GetLength(0);
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void TurnSwitch()
    {
        for(int i = 0; i < LiftNum; i++)
        {
            if (Lift[i].LiftFlag == false)
            {
                Lift[i].LiftFlag = true;
            }
            else
            {
                Lift[i].LiftFlag = false;
            }
        }
    }
}
