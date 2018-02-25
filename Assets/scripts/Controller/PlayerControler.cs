﻿using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{



    private Rigidbody2D rigidbody2D;
    public Animator anim;
    public float WalkSpeed = 4f;

    private bool isGrounded;
    public LayerMask groundLayer; //Linecastで判定するLayer
    public LayerMask liftLayer;
    public float jumpPower = 400;
    public float DashSpeed = 8f;
    private float jumpwait = 0f;
    public bool jumpflag = true;

    private bool isHashigo = false;
    public float HashigoSpeed = 3f;

    public float Level = 10;

    public bool isLifted;
    public bool isGotten = false;

    private float TotalSpeed;

    private GameObject GObj;

    float now = 0;
    public PlayerOn foot;

    public enum playerType
    {
        Ed,
        Dola
    }

    public playerType player = playerType.Ed;


    public Vector2 direction = new Vector2(1.0f, 0.0f);
    public LayerMask mask;
    public float RayRangeToFoot;

    // Use this for initialization
    void Start()
    {
        GObj = GameObject.Find("Lift");
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (foot.OnLift == true)
        {
            isLifted = true;
            isGrounded = true;
        }
        else
        {
            isLifted = false;
        }


        if (player == playerType.Dola)
        {
            anim.SetBool("Dola", true);
        }
        else
        {
            anim.SetBool("Dola", false);
        }

        //デバッグ用ブレークポイント
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("break");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData.SetFloat("Y", this.transform.position.y);
            Debug.Log("Yに" + this.transform.position.y + "をセーブしました");
        }

        if (isGrounded == false)
        {
            isGrounded = Physics2D.Linecast(
                transform.position + transform.up * 0.002f,
                transform.position - transform.up * 0.002f,
                groundLayer);
        }

        //jumpflag:一定時間経過&&地面と接地

        if (now >= 0.5f)
        {
            now = 0;
            jumpflag = true;
            Debug.Log("jumpflag_ON");
        }

        if (jumpflag == false)
        {
            now += Time.deltaTime;
        }

        if (Input.GetKeyDown("space") && player == playerType.Dola)
        {
            if (jumpflag)
            {
                if (isGrounded)
                {
                    if (player == playerType.Ed)
                    {
                        jumpPower = 250;
                    }
                    else
                    {
                        jumpPower = 350;
                    }
                    jumpflag = false;
                    isGrounded = false;
                    rigidbody2D.AddForce(Vector2.up * jumpPower);
                    Debug.Log("jumpflag_OFF");
                }
            }
        }

        //左キー: -1、右キー: 1
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //左か右を入力したら
        if (x != 0)
        {

            Ray2D rayToFoot = new Ray2D(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(x*2, -1.5f));
            RaycastHit2D hitToFoot = Physics2D.Raycast((Vector2)rayToFoot.origin, (Vector2)rayToFoot.direction, RayRangeToFoot, mask);

            Debug.DrawRay(rayToFoot.origin, rayToFoot.direction * RayRangeToFoot, Color.blue, 0.1f, false);

            if (hitToFoot.collider != null || player == playerType.Dola)
            {
                //入力方向へ移動
                if (Input.GetKey(KeyCode.LeftShift))//ダッシュ
                {
                    if (isLifted)
                    {
                        LiftController LC = GObj.GetComponent<LiftController>();
                        TotalSpeed = x * DashSpeed + LC.rigidbody2D.velocity.x;
                        rigidbody2D.velocity = new Vector2(TotalSpeed, rigidbody2D.velocity.y);
                    }
                    else if (isHashigo)
                    {
                        if (y != 0)
                        {
                            rigidbody2D.velocity = new Vector2(x * WalkSpeed, y * HashigoSpeed);
                        }
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2(x * DashSpeed, rigidbody2D.velocity.y);
                    }
                    Vector2 temp = transform.localScale;
                    temp.x = x * 3;
                    transform.localScale = temp;
                    anim.SetBool("Walk", true);
                }
                else
                {
                    if (isLifted)
                    {
                        LiftController LC = foot.Lift.GetComponent<LiftController>();
                        rigidbody2D.velocity = new Vector2(x * WalkSpeed + LC.rigidbody2D.velocity.x, LC.rigidbody2D.velocity.y);
                        //if (LC.LiftFlag == true)
                        //{
                        //    if (LC.XLift)
                        //    {
                        //        rigidbody2D.velocity = new Vector2(TotalSpeed, rigidbody2D.velocity.y);
                        //    }
                        //    else
                        //    {
                        //        TotalSpeed = x * WalkSpeed;
                        //        rigidbody2D.velocity = new Vector2(TotalSpeed, rigidbody2D.velocity.y);
                        //    }
                        //}
                        //else
                        //{
                        //    TotalSpeed = x * WalkSpeed;
                        //    rigidbody2D.velocity = new Vector2(TotalSpeed, rigidbody2D.velocity.y);
                        //}
                    }
                    else if (isHashigo)
                    {
                        rigidbody2D.velocity = new Vector2(x * WalkSpeed, y * HashigoSpeed);
                    }
                    else
                    {
                        rigidbody2D.velocity = new Vector2(x * WalkSpeed, rigidbody2D.velocity.y);
                    }

                    Vector2 temp = transform.localScale;
                    temp.x = x * 3;
                    transform.localScale = temp;
                    anim.SetBool("Walk", true);
                }
            }
        }
        else
        {
            if (isLifted)
            {
                LiftController LC = foot.Lift.GetComponent<LiftController>();
                rigidbody2D.velocity = new Vector2(0 + LC.rigidbody2D.velocity.x, LC.rigidbody2D.velocity.y);

                //if (LC.LiftFlag == true)
                //{
                //    TotalSpeed = LC.nowLiftSpeed;
                //    if (LC.YLift)
                //    {
                //        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y+TotalSpeed);
                //    }
                //    else
                //    {
                //        rigidbody2D.velocity = new Vector2(TotalSpeed, rigidbody2D.velocity.y);
                //    }
                //}
                //else
                //{
                //    rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                //}
            }
            else if (isHashigo)
            {
                rigidbody2D.velocity = new Vector2(x * WalkSpeed, y * HashigoSpeed);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
            }
            anim.SetBool("Walk", false);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "key(with gate)")
        {
            isGotten = true;
        }
        else if (collision.gameObject.name == "Hashigo")
        {
            isHashigo = true;
            Debug.Log("isHashigo_ON");
            rigidbody2D.gravityScale = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.name == "Lift")
        //{
        //    isLifted = false;
        //    Debug.Log("isLifted_OFF");
        //}
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hashigo")
        {
            isHashigo = false;
            Debug.Log("isHashigo_OFF");
            rigidbody2D.gravityScale = 2;
        }
    }

}