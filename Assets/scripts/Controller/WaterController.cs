using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    Rigidbody2D L;
    Rigidbody2D R;

    public GameObject Left;
    public GameObject Right;

    public GameObject Gate_a;
    public GameObject Gate_b;
    public GameObject Gate_c;

    public float high;
    public float middle;
    public float low;

    public float liftSpeed;

    public bool EventFlag;

    public float BlockSpeed;

    public enum eventType
    {
        AtoB,
        BtoC,
        CtoB,
        BtoA
    }

    public eventType Event;

    // Use this for initialization
    void Start()
    {
        L = Left.GetComponent<Rigidbody2D>();
        R = Right.GetComponent<Rigidbody2D>();
        Event = eventType.AtoB;
        EventFlag = false;
        Gate_a.SetActive(false);
        Gate_b.SetActive(true);
        Gate_c.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            Debug.Log(L.velocity);
        }
        if (EventFlag == true)
        {
            Vector2 left = Left.transform.position;
            Vector2 right = Right.transform.position;
            Vector3 High = new Vector3(0, high, 0);
            Vector3 Middle = new Vector3(0, middle, 0);
            Vector3 Low = new Vector3(0, low, 0);
            Gate_a.SetActive(true);
            Gate_b.SetActive(true);
            Gate_c.SetActive(true);
            switch (Event)
            {
                case eventType.AtoB:
                    L.velocity = new Vector2(0, -2);
                    R.velocity = new Vector2(0, 2);
                    if (left.y < middle)
                    {
                        L.velocity = new Vector2(0, 0);
                        R.velocity = new Vector2(0, 0);
                        left.y = middle;
                        right.y = middle;
                        EventFlag = false;
                        Gate_a.SetActive(true);
                        Gate_b.SetActive(false);
                        Gate_c.SetActive(true);
                    }
                    break;
                case eventType.BtoC:
                    L.velocity = new Vector2(0, -2);
                    R.velocity = new Vector2(0, 2);
                    if (left.y < low)
                    {
                        L.velocity = new Vector2(0, 0);
                        R.velocity = new Vector2(0, 0);
                        left.y = low;
                        right.y = high;
                        EventFlag = false;
                        Gate_a.SetActive(true);
                        Gate_b.SetActive(true);
                        Gate_c.SetActive(false);
                    }
                    break;
                case eventType.CtoB:
                    L.velocity = new Vector2(0, 2);
                    R.velocity = new Vector2(0, -2);
                    if (left.y > middle)
                    {
                        L.velocity = new Vector2(0, 0);
                        R.velocity = new Vector2(0, 0);
                        left.y = middle;
                        right.y = middle;
                        EventFlag = false;
                        Gate_a.SetActive(true);
                        Gate_b.SetActive(false);
                        Gate_c.SetActive(true);
                    }
                    break;
                case eventType.BtoA:
                    L.velocity = new Vector2(0, 2);
                    R.velocity = new Vector2(0, -2);
                    if (left.y > high)
                    {
                        L.velocity = new Vector2(0, 0);
                        R.velocity = new Vector2(0, 0);
                        left.y = high;
                        right.y = low;
                        EventFlag = false;
                        Gate_a.SetActive(false);
                        Gate_b.SetActive(true);
                        Gate_c.SetActive(true);
                    }
                    break;
            }
            Left.transform.position = left;
            Right.transform.position = right;
        }

    }
    //public void Aaction()
    //{
    //    Gate_a.SetActive(false);
    //    Gate_b.SetActive(true);
    //    Gate_c.SetActive(true);
    //    Vector2 left = Left.transform.position;
    //    Vector2 right = Right.transform.position;
    //    left.y = high;
    //    right.y = low;
    //    Left.transform.position = left;
    //    Right.transform.position = right;
    //}
    //public void Baction()
    //{
    //    Gate_a.SetActive(true);
    //    Gate_b.SetActive(false);
    //    Gate_c.SetActive(true);
    //    Vector2 left = Left.transform.position;
    //    Vector2 right = Right.transform.position;
    //    left.y = middle;
    //    right.y = middle;
    //    Left.transform.position = left;
    //    Right.transform.position = right;
    //}
    //public void Caction()
    //{
    //    Gate_a.SetActive(true);
    //    Gate_b.SetActive(true);
    //    Gate_c.SetActive(false);
    //    Vector2 left = Left.transform.position;
    //    Vector2 right = Right.transform.position;
    //    left.y = low;
    //    right.y = high;
    //    Left.transform.position = left;
    //    Right.transform.position = right;
    //}
}
