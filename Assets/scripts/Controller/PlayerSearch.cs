using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour {

    private bool CollisionFlag;
    private bool SwitchFlag;
    private bool RockFlag;
    private bool PCFlag;
    private bool waterFlag;
    private bool keyFlag;
    private bool Switch_GarekiFlag;
    private bool BridgeFlag;
    private bool doorFlag;

    public GameObject Balloon;
    public GameObject obj;
    public PlayerControler PC;

    public bool plessureFlag;

    public WaterController water;
    public int waterType = 0;

    public KeyContoller KC;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {


        if (CollisionFlag == true)
        {
            Balloon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (SwitchFlag == true)
                {
                    SwitchController Switch = obj.gameObject.GetComponent <SwitchController> ();
                    Switch.TurnSwitch();
                }
                else if (RockFlag == true)
                {
                    obj.gameObject.SetActive(false);
                }
                else if (Switch_GarekiFlag == true)
                {
                    SwitchController Switch = obj.gameObject.GetComponent<SwitchController>();
                    Switch.TurnSwitch();
                    obj.SetActive(false);
                }
                else if (BridgeFlag == true)
                {
                    BridgeController Bridge = obj.gameObject.GetComponent<BridgeController>();
                    Bridge.BridgeAction();
                }
                else if (PCFlag == true)
                {
                    if (PC.player == PlayerControler.playerType.Ed)
                    {
                        PC.player = PlayerControler.playerType.Dola;
                    }
                    else
                    {
                        PC.player = PlayerControler.playerType.Ed;
                    }
                }
                else if (doorFlag == true)
                {
                    doorController dc = obj.gameObject.GetComponent<doorController>();
                    dc.Moving();
                }
                else if (waterFlag == true)
                {
                    waterType++;
                    waterType %= 4;
                    switch (waterType)
                    {
                        case 0:
                            water.Event = WaterController.eventType.BtoA;
                            break;
                        case 1:
                            water.Event = WaterController.eventType.AtoB;
                            break;
                        case 2:
                            water.Event = WaterController.eventType.BtoC;
                            break;
                        case 3:
                            water.Event = WaterController.eventType.CtoB;
                            break;
                    }
                    water.EventFlag = true;
                }
                else if (keyFlag == true)
                {
                    if (KC.open == true)
                    {
                        KC.gateClose();
                    }
                    else
                    {
                        KC.gateOpen();
                    }
                }
            }
        }
        else
        {
            Balloon.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "switch" && PC.player == PlayerControler.playerType.Ed)
        {
            CollisionFlag = true;
            SwitchFlag = true;
            obj = other.gameObject;

        }
        //Gareki
        if (other.tag == "switch_Gareki" && PC.player == PlayerControler.playerType.Dola)
        {
            CollisionFlag = true;
            Switch_GarekiFlag = true;
            obj = other.gameObject;

        }
        if (other.tag == "Bridge" && PC.player == PlayerControler.playerType.Dola)
        {
            BridgeController Bridge = other.gameObject.GetComponent<BridgeController>();
            if (Bridge.BridgeActionFlag == false)
            {
                CollisionFlag = true;
                BridgeFlag = true;
                obj = other.gameObject;
            }
        }
        if (other.tag == "rock"&& PC.player == PlayerControler.playerType.Dola)
        {
            CollisionFlag = true;
            RockFlag = true;
            obj = other.gameObject;
        }
        if (other.gameObject.name == "PlayerChange")
        {
            CollisionFlag = true;
            PCFlag = true;
        }
        if (other.gameObject.name == "Lift")
        {
            LiftController LC = other.GetComponent<LiftController>();
            if (LC.lifttype == LiftController.LiftType.Pressure)
            {
                plessureFlag = true;
                Debug.Log("plessureON");
            }
        }
        if (other.gameObject.name == "waterController" && PC.player == PlayerControler.playerType.Ed)
        {
            CollisionFlag = true;
            waterFlag = true;
        }
        if (other.gameObject.name == "key" && PC.player == PlayerControler.playerType.Ed)
        {
            CollisionFlag = true;
            KC = other.GetComponent<KeyContoller>();
            keyFlag = true;
        }
        if (other.gameObject.tag == "door")
        {
            CollisionFlag = true;
            doorFlag = true;
            obj = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "switch")
        {
            CollisionFlag = false;
            SwitchFlag = false;
        }
        if (collision.tag == "rock")
        {
            CollisionFlag = false;
            RockFlag = false;
        }
        if (collision.tag == "switch_Gareki")
        {
            CollisionFlag = false;
            Switch_GarekiFlag = false;
        }
        if (collision.tag == "Bridge")
        {
            CollisionFlag = false;
            BridgeFlag = false;
        }
        if (collision.gameObject.name == "PlayerChange")
        {
            CollisionFlag = false;
            PCFlag = false;
        }
        if (collision.gameObject.name == "Lift")
        {
            plessureFlag = false;
        }
        if (collision.gameObject.name == "waterController")
        {
            CollisionFlag = false;
            waterFlag = false;
        }
        if (collision.gameObject.name == "key")
        {
            CollisionFlag = false;
            keyFlag = false;
        }
        if (collision.gameObject.tag == "door")
        {
            CollisionFlag = false;
            doorFlag = false;
        }
    }
}
