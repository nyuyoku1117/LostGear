<<<<<<< HEAD
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> a8ab6369bcc0422b3d5083f4479219dc71d85108
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public LiftController[] Lift;
	public GameObject arrow;

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

				var heading = Lift [i].transform.position - this.transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance;
				var CurrentDirection = direction * 2;

                var diff = (Lift[i].transform.position - transform.position).normalized;
                var rote = Quaternion.FromToRotation(Vector3.up, diff);

                Instantiate(arrow, CurrentDirection + this.transform.position, rote);


            }
            else
            {
                Lift[i].LiftFlag = false;

                var heading = Lift[i].transform.position - this.transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;
                var CurrentDirection = direction * 2;

                var diff = (Lift[i].transform.position - transform.position).normalized;
                var rote = Quaternion.FromToRotation(Vector3.up, diff);

                Instantiate(arrow, CurrentDirection + this.transform.position, rote);


            }
        }
    }
}
<<<<<<< HEAD
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public LiftController[] Lift;
	public GameObject arrow;

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

				var heading = Lift [i].transform.position - this.transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance;
				var CurrentDirection = direction * 2;

				var diff = (Lift[i].transform.position - transform.position ).normalized;
				var rote = Quaternion.FromToRotation( Vector3.up,  diff);

				Instantiate (arrow, CurrentDirection + this.transform.position, rote);


            }
            else
            {
                Lift[i].LiftFlag = false;

				var heading = Lift [i].transform.position - this.transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance;
				var CurrentDirection = direction * 2;

				var diff = (Lift[i].transform.position - transform.position ).normalized;
				var rote = Quaternion.FromToRotation( Vector3.up,  diff);

				Instantiate (arrow, CurrentDirection + this.transform.position, rote);


            }
        }
    }
}
>>>>>>> origin/master
=======
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public LiftController[] Lift;
	public GameObject arrow;

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

				var heading = Lift [i].transform.position - this.transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance;
				var CurrentDirection = direction * 2;

				var diff = (Lift[i].transform.position - transform.position ).normalized;
				var rote = Quaternion.FromToRotation( Vector3.up,  diff);

				Instantiate (arrow, CurrentDirection + this.transform.position, rote);


            }
            else
            {
                Lift[i].LiftFlag = false;

				var heading = Lift [i].transform.position - this.transform.position;
				var distance = heading.magnitude;
				var direction = heading / distance;
				var CurrentDirection = direction * 2;

				var diff = (Lift[i].transform.position - transform.position ).normalized;
				var rote = Quaternion.FromToRotation( Vector3.up,  diff);

				Instantiate (arrow, CurrentDirection + this.transform.position, rote);


            }
        }
    }
}
>>>>>>> origin/master
>>>>>>> a8ab6369bcc0422b3d5083f4479219dc71d85108
