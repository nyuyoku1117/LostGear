using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {

	public float walkspeed = 1;
	public float alertwalkspeed = 2;
	public Vector2 direction = new Vector2 (1.0f, 0.0f);
	public bool Alert = false;
	public Rigidbody2D rigidbody2D;
	public LayerMask mask;
	public float RayRangeToPlayer;
	public float RayRangeToFoot;

	// Use this for initialization
	void Start () {

		rigidbody2D = GetComponent<Rigidbody2D> ();
		rigidbody2D.velocity = new Vector2(1.0f,0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		
		rigidbody2D.velocity = direction * (Alert ? alertwalkspeed : walkspeed);

		Ray2D rayToPlayer = new Ray2D (transform.position, (Vector2)rigidbody2D.velocity);
		Ray2D rayToFoot = new Ray2D (new Vector2(transform.position.x,transform.position.y - 1f), new Vector2(rigidbody2D.velocity.x,-1f));

		RaycastHit2D hitToPlayer = Physics2D.Raycast((Vector2)rayToPlayer.origin,(Vector2)rayToPlayer.direction,RayRangeToPlayer,mask);
		RaycastHit2D hitToFoot = Physics2D.Raycast ((Vector2)rayToFoot.origin, (Vector2)rayToFoot.direction, RayRangeToFoot, mask);

		Debug.DrawRay (rayToPlayer.origin,rayToPlayer.direction*RayRangeToPlayer, Color.red,0.1f, false);
		Debug.DrawRay (rayToFoot.origin, rayToFoot.direction * RayRangeToFoot, Color.blue, 0.1f, false);
		//Debug.Log (ray.origin + "__" + ray.direction + "__" + RayRangeToPlayer);

		if (hitToPlayer.collider) {

			if (hitToPlayer.collider.gameObject.tag != "Player") {

				Alert = false;
				Debug.Log ("NonRayCast");


			} else if (hitToPlayer.collider.gameObject.tag == "Player") {

				Debug.Log ("RayCastPlayer");
				Alert = true;

			}
		} else {
			Alert = false;
		}

		if (hitToFoot.collider) {

			if (hitToFoot.collider==null) {
                direction *= -1;
            }

		}


	}


	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "Player") {

			Debug.Log ("game_over");
			FadeManager.Instance.LoadLevel ("Title", 1f);

		}
	}

	void OnCollisionEnter2D(Collision2D col){

		direction *= -1;

	}
}
