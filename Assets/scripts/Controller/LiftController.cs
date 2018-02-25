using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{

    public Rigidbody2D rigidbody2D;
    public float LiftSpeed = 3f;
    public float LiftStartPosition = 5f;
    public float LiftRange = 10f;
    public float StopTime = 0;
    public float LiftTime = 0;
    private float Y;
    private float X;
    public bool XLift = false;
    public bool YLift = true;
    public enum LiftType {
        OneWay,
        Loop,
        Switch,
        Pressure
    }
    public bool LiftFlag;
    private bool plessured;
    private bool plessureEvent;
    private int stayTime = 2;
    private float h = 0;
    private float i = 0;
    private float end_x;
    private float end_y;
    private GameObject player;

    public LiftType lifttype = LiftType.OneWay;

    // Use this for initialization
    void Start() {
        plessured = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        Vector2 start = rigidbody2D.transform.position;
        end_x = start.x;
        end_y = start.y;
    }

    // Update is called once per frame
    void Update()
    {
        switch (lifttype)
        {

            case LiftType.OneWay:
                ///一方通行のリフト
                ///現在自動的にスタートする状態

                if (LiftTime < LiftRange / System.Math.Abs(LiftSpeed))
                {
                    LiftTime += Time.deltaTime;

                    if (YLift)
                    {

                        rigidbody2D.velocity = new Vector2(0, LiftSpeed);

                    }
                    else if (XLift)
                    {
                        rigidbody2D.velocity = new Vector2(LiftSpeed, 0);
                    }
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                }

                break;

            case LiftType.Loop:
                ///ループするリフト

                if (LiftTime < LiftRange / System.Math.Abs(LiftSpeed))
                {
                    h += Time.deltaTime;

                    if (h > stayTime)
                    {
                        LiftTime += Time.deltaTime;
                        if (YLift)
                        {

                            rigidbody2D.velocity = new Vector2(0, LiftSpeed);

                        }
                        else if (XLift)
                        {

                            rigidbody2D.velocity = new Vector2(LiftSpeed, 0);

                        }
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2(0, 0);
                    }

                }
                else if (LiftTime < (LiftRange * 2) / System.Math.Abs(LiftSpeed))//折り返し
                {
                    i += Time.deltaTime;
                    if (i > stayTime)
                    {
                        LiftTime += Time.deltaTime;
                        if (YLift)
                        {
                            rigidbody2D.velocity = new Vector2(0, LiftSpeed * (-1));
                        }
                        else if (XLift)
                        {
                            rigidbody2D.velocity = new Vector2(LiftSpeed * (-1), 0);
                        }
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2(0, 0);
                    }
                }
                else
                {
                    rigidbody2D.transform.position = new Vector2(end_x,end_y);
                    rigidbody2D.velocity = new Vector2(0, 0);
                    LiftTime = 0;
                    i = 0;
                    h = 0;
                }

                break;

            case LiftType.Switch:
                //スイッチ式のリフト
                //現在はループと同じ
                if (LiftFlag == true)//リフトがオンでなければ動作しない
                {
                    if (LiftTime < LiftRange / System.Math.Abs(LiftSpeed))
                    {
                        h += Time.deltaTime;

                        if (h > stayTime)
                        {
                            LiftTime += Time.deltaTime;
                            if (YLift)
                            {

                                rigidbody2D.velocity = new Vector2(0, LiftSpeed);

                            }
                            else if (XLift)
                            {

                                rigidbody2D.velocity = new Vector2(LiftSpeed, 0);

                            }
                        }
                        else
                        {
                            rigidbody2D.velocity = new Vector2(0, 0);
                        }

                    }
                    else if (LiftTime < (LiftRange * 2) / System.Math.Abs(LiftSpeed))//折り返し
                    {
                        i += Time.deltaTime;

                        if (i > stayTime)
                        {
                            LiftTime += Time.deltaTime;
                            if (YLift)
                            {

                                rigidbody2D.velocity = new Vector2(0, LiftSpeed * (-1));

                            }
                            else if (XLift)
                            {
                                rigidbody2D.velocity = new Vector2(LiftSpeed * (-1), 0);

                            }
                        }
                        else
                        {
                            rigidbody2D.velocity = new Vector2(0, 0);
                        }
                    }
                    else
                    {
                        rigidbody2D.transform.position = new Vector2(end_x, end_y);
                        rigidbody2D.velocity = new Vector2(0, 0);
                        LiftTime = 0;
                        h = 0;
                        i = 0;
                    }
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                }
                break;

            case LiftType.Pressure:
                player = GameObject.Find("player");
                PlayerControler PC = player.GetComponent<PlayerControler>();
                PlayerSearch PS = player.GetComponentInChildren<PlayerSearch>();
                if (LiftFlag == true)//リフトがオンでなければ動作しない
                {
                    if (plessured == true && PC.isLifted == true)
                    {
                        plessureEvent = true;
                    }
                    if (plessureEvent == true)
                    {
                        if (LiftTime < LiftRange / System.Math.Abs(LiftSpeed))
                        {
                            LiftTime += Time.deltaTime;
                            if (YLift)
                            {
                                rigidbody2D.velocity = new Vector2(0, LiftSpeed);

                            }
                            else if (XLift)
                            {
                                rigidbody2D.velocity = new Vector2(LiftSpeed, 0);
                            }
                        }
                        else if (LiftTime < (LiftRange * 2) / System.Math.Abs(LiftSpeed))//折り返し
                        {
                            if (PS.plessureFlag == false)
                            {
                                LiftTime += Time.deltaTime;
                                if (YLift)
                                {

                                    rigidbody2D.velocity = new Vector2(0, LiftSpeed * (-1));

                                }
                                else if (XLift)
                                {
                                    rigidbody2D.velocity = new Vector2(LiftSpeed * (-1), 0);
                                }
                            }
                            else
                            {
                                rigidbody2D.velocity = new Vector2(0, 0);
                            }
                        }
                        else
                        {
                            rigidbody2D.transform.position = new Vector2(end_x, end_y);
                            rigidbody2D.velocity = new Vector2(0, 0);
                            LiftTime = 0;
                            plessureEvent = false;
                        }
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2(0, 0);
                    }

                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            plessured = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            plessured = false;
        }
    }
}
