using System.Collections;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

	public bool player_Active = true;
	public bool menu_Active = false;

	[SerializeField]
	public GameObject MenuUI;
	private GameObject instanceMenuUI;

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

	private int currentMENUpointa = 1;
	float waittime = 0;
	private bool moveflag = true;
	public GameObject MENUPointa;
	private bool MapFlag = false;
	private bool ArrowFlag = true;

	public GameObject[] MAP1Pictures;
	public bool[] Map1flag;
	public Vector3[] Map1_position;
	public GameObject canvas;
	public GameObject[] MAP1;
	public RectTransform firstMenuPointa;
	private float mapscroll = 0;
	//canvas上の一番上にくるものを指定する際のindex用
	public GameObject CanvasFrontObject;



    // Use this for initialization
    void Start()
    {
		player_Active = true;
		menu_Active = false;
        GObj = GameObject.Find("Lift");
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

		//Map1_11 Start
		for (int i = 0; i < 20; i++) {
			//Map1flag [i] = false;
			if (i < 10) {
				Map1_position [i] = new Vector3 (-150 + (i) * 100, 50, 0);
			} else {
				Map1_position [i] = new Vector3 (-150 + (i - 10) * 100, -50, 0);
			}
		}

		Map1flag [10] = true;
		//マップのセーブデータ読み込み
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
            WalkSpeed = 6f;
        }
        else
        {
            anim.SetBool("Dola", false);
            WalkSpeed = 4f;
        }

        //デバッグ用ブレークポイント
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("break");
        }

		//save
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveData.SetFloat("Y", this.transform.position.y);
            Debug.Log("Yに" + this.transform.position.y + "をセーブしました");
        }

		//メニュー用
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (player_Active) {
			
				menu_Active = true;
				player_Active = false;

				anim.SetBool ("Walk", false);

				//if (instanceMenuUI == null) {
				
					//instanceMenuUI = GameObject.Instantiate (MenuUI) as GameObject;
				MenuUI.gameObject.SetActive(true);

				//}
				Debug.Log ("MENUOPEN");
			
			} else {

				player_Active = true;
				menu_Active = false;
				MapFlag = false;
				ArrowFlag = true;
				currentMENUpointa = 1;
				firstMenuPointa.localPosition = new Vector3 (0.92f, 80, 0); 
				//MENUPointa.transform.position = RectTransformUtility.WorldToScreenPoint (Camera.main, new Vector3 (0, 0, 0));
				for(int i=0;i<20;i++){
					if(Map1flag[i] == true){

						Destroy(MAP1[i]);

						//MAPDELETE
					}
				}
				MenuUI.gameObject.SetActive (false);
				//Destroy (instanceMenuUI);
				Debug.Log ("MENUCLOSE");

			}


		}

		//playerがアクティブであれば操作可能(MENUの際off)
		if (player_Active) {

			//着地判定
			if (isGrounded == false) {
				isGrounded = Physics2D.Linecast (
					transform.position + transform.up * 0.02f,
					transform.position - transform.up * 0.02f,
					groundLayer);
			}

			//jumpflag:一定時間経過&&地面と接地

			if (now >= 0.5f) {
				now = 0;
				jumpflag = true;
				Debug.Log ("jumpflag_ON");
			}

			if (jumpflag == false) {
				now += Time.deltaTime;
			}

			if (Input.GetKeyDown ("space") && player == playerType.Dola) {
				if (jumpflag) {
					if (isGrounded) {
						if (player == playerType.Ed) {
							jumpPower = 250;
						} else {
							jumpPower = 350;
						}
						jumpflag = false;
						isGrounded = false;
						rigidbody2D.AddForce (Vector2.up * jumpPower);
						Debug.Log ("jumpflag_OFF");
					}
				}
			}

			//左キー: -1、右キー: 1
			float x = Input.GetAxisRaw ("Horizontal");
			float y = Input.GetAxisRaw ("Vertical");

			//左か右を入力したら
			if (x != 0) {

				Ray2D rayToFoot = new Ray2D (new Vector2 (transform.position.x, transform.position.y + 0.5f), new Vector2 (x * 2, -1.5f));
				RaycastHit2D hitToFoot = Physics2D.Raycast ((Vector2)rayToFoot.origin, (Vector2)rayToFoot.direction, RayRangeToFoot, mask);

				Debug.DrawRay (rayToFoot.origin, rayToFoot.direction * RayRangeToFoot, Color.blue, 0.1f, false);

				if (hitToFoot.collider != null || player == playerType.Dola) {
					//入力方向へ移動
					if (Input.GetKey (KeyCode.LeftShift)) {//ダッシュ
						if (isLifted) {
							LiftController LC = GObj.GetComponent<LiftController> ();
							TotalSpeed = x * DashSpeed + LC.rigidbody2D.velocity.x;
							rigidbody2D.velocity = new Vector2 (TotalSpeed, rigidbody2D.velocity.y);
						} else if (isHashigo) {
							if (y != 0) {
								rigidbody2D.velocity = new Vector2 (x * WalkSpeed, y * HashigoSpeed);
							}
						} else {
							rigidbody2D.velocity = new Vector2 (x * DashSpeed, rigidbody2D.velocity.y);
						}
						Vector2 temp = transform.localScale;
						temp.x = x * (-5);
						transform.localScale = temp;
						anim.SetBool ("Walk", true);
					} else {
						if (isLifted) {
							LiftController LC = foot.Lift.GetComponent<LiftController> ();
							rigidbody2D.velocity = new Vector2 (x * WalkSpeed + LC.rigidbody2D.velocity.x, LC.rigidbody2D.velocity.y);
						} else if (isHashigo) {
							rigidbody2D.velocity = new Vector2 (x * WalkSpeed, y * HashigoSpeed);
						} else {
							rigidbody2D.velocity = new Vector2 (x * WalkSpeed, rigidbody2D.velocity.y);
						}

						Vector2 temp = transform.localScale;
						temp.x = x * (-5);
						transform.localScale = temp;
						anim.SetBool ("Walk", true);
					}
				}
			} else {
				if (isLifted) {
					LiftController LC = foot.Lift.GetComponent<LiftController> ();
					rigidbody2D.velocity = new Vector2 (0 + LC.rigidbody2D.velocity.x, LC.rigidbody2D.velocity.y);
				} else if (isHashigo) {
					rigidbody2D.velocity = new Vector2 (x * WalkSpeed, y * HashigoSpeed);
				} else {
					rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
				}
				anim.SetBool ("Walk", false);
			}
		} else {
            
			//MENU画面操作
			if (menu_Active) {

				if (moveflag == false) {

					waittime += Time.deltaTime;

				}

				if (waittime > 1.5) {
					moveflag = true;
					waittime = 0;
				}

				if (ArrowFlag) {
					if (Input.GetKeyDown (KeyCode.DownArrow)) {

						if (currentMENUpointa != 4) {
							if (moveflag) {
								currentMENUpointa += 1;
								Debug.Log (currentMENUpointa);
								iTween.MoveBy (MENUPointa.gameObject, iTween.Hash ("y", -50));
								moveflag = false;
							}
						}
					}

					if (Input.GetKeyDown (KeyCode.UpArrow)) {

						if (currentMENUpointa != 1) {
							if (moveflag) {
								currentMENUpointa -= 1;
								Debug.Log (currentMENUpointa);
								iTween.MoveBy (MENUPointa.gameObject, iTween.Hash ("y", 50f));
								moveflag = false;
							}
						}
					}
				}

				if (MapFlag) {

					float x = Input.GetAxisRaw ("Horizontal");
					if (x != 0) {
						
						for (int i = 0; i < 20; i++) {
							if (Map1flag [i]) {
								iTween.MoveBy (MAP1 [i], iTween.Hash ("x", x * (-65f), "Time", 0.2f));
							}
						}
					}





				}

				if (currentMENUpointa == 1) {

					if (Input.GetKeyDown (KeyCode.Z)) {
						
						if (MapFlag == false) {


							ArrowFlag = false;
							Debug.Log ("MAP!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
							MapFlag = true;

							//MAPShow

							for (int i = 0; i < 20; i++) {

								if (Map1flag[i] == true) {

									var rote = Quaternion.AngleAxis (0, new Vector3(0,0,0));
									MAP1[i] = Instantiate (MAP1Pictures[i], Map1_position [i], rote) as GameObject;
									//MAP1[i].transform.position = RectTransformUtility.WorldToScreenPoint (Camera.main, new Vector3 (0, 0, 0));
									MAP1[i].transform.SetParent (canvas.transform, false);
									MAP1 [i].transform.SetSiblingIndex (CanvasFrontObject.transform.GetSiblingIndex() + 1);
								}


							}





						} else {

							ArrowFlag = true;



							Debug.Log ("MAPBREAK!!!!!!!!!!!!!!!!!!!!!!!!!!");
							MapFlag = false;

							for(int i=0;i<20;i++){
								if(Map1flag[i] == true){
								
									Destroy(MAP1[i]);
								
									//MAPDELETE
								}
							}
						}
					}

				} else if (currentMENUpointa == 2) {



				} else if (currentMENUpointa == 3) {



				} else if (currentMENUpointa == 4) {



				}


			}

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

	void OnTriggerEnter2D(Collider2D col){

		for (int i = 0; i < 20; i++) {
			
			if (col.gameObject.name == "Area" + (i+1)) {
				Map1flag [i] = true;
				Debug.Log ("Area" + (i+1) + "OPEN!");
			}


		}
	}
}
