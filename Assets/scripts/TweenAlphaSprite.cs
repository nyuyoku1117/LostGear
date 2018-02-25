using System.Collections;
using UnityEngine;

/// <summary>
/// Unity2DのSpriteをフェードイン・アウトさせるTweener
/// </summary>

[AddComponentMenu("WestHill/Tween/AlphaSprite")]
[RequireComponent(typeof(SpriteRenderer))]

public class TweenAlphaSprite : MonoBehaviour {

	//再生形式
	public enum PLAY_STYLE
	{
		//一回のみ
		Once,
		//繰り返し
		Loop,
		//逆再生繰り返し
		PingPong,
	}

	/// <summary>
	///フェード開始時のAlpha値(0~1) 
	/// </summary>
	public float fromAlpha = 0f;

	/// <summary>
	/// フェード終了時のAlpha値(0~1)
	/// </summary>
	public float toAlpha = 1f;

	/// <summary>
	/// フェード時間
	/// </summary>
	public float duration= 0f;

	/// <summary>
	/// 開始までのディレイ
	/// </summary>
	public float startDelay = 0f;

	/// <summary>
	/// 再生形式
	/// </summary>
	public PLAY_STYLE playstyle = PLAY_STYLE.Once;

	/// <summary>
	/// アニメーション終了後に通知を投げたいイベントレシーバ	
	/// </summary>
	public GameObject eventReciever;

	/// <summary>
	/// アニメーション終了後に投げる通知メソッド名
	/// </summary>
	public string callWhenFinished;

	//起動と同時に再生するフラグ
	[SerializeField]
	bool PlayOnAwake;

	//自身のSpriteRenderer
	SpriteRenderer spriterenderer;

	//一時color
	Color tempColor = Color.white;

	//Duration計測用
	float durationTimer;

	//ディレイ計測用
	float delayTimer;

	//逆再生フラグ
	bool reverse;

	/*
	void Awake(){

		if (PlayOnAwake) {
			Init ();
			Play ();
		} else {
			Stop ();
		}

	}

*/
	// Update is called once per frame
	void Update () {
		if (spriterenderer == null) {
			return;
		}

		//ディレイ
		if (0f < delayTimer) {
			delayTimer -= Time.deltaTime;
			return;
		}

		//TimeUpdate
		durationTimer -= Time.deltaTime;
		float nowTime = 1f - (durationTimer / duration);

		//Fade
		tempColor.a = reverse ? Mathf.Lerp ( toAlpha,fromAlpha,nowTime):Mathf.Lerp(fromAlpha,toAlpha,nowTime);
		spriterenderer.color = tempColor;

		//Fade終了時
		float finishAlpha = reverse ? fromAlpha : toAlpha;
		if (tempColor.a == finishAlpha){

			//再生形式毎処理
			switch (playstyle) {
			case PLAY_STYLE.Once:
				//通知設定がある場合は通知を投げる
				if (eventReciever != null && !string.IsNullOrEmpty (callWhenFinished)) {

					eventReciever.SendMessage (callWhenFinished, SendMessageOptions.DontRequireReceiver);

				}

				//停止
				Stop ();
				break;

			case PLAY_STYLE.Loop:
				//ループ
				Init ();
				break;

			case PLAY_STYLE.PingPong:
				//逆再生
				reverse = !reverse;
				Init();
				break;

			}
		}
	}


	void OnEnable(){
		Init ();
	}

	void Init(){
		spriterenderer = spriterenderer == null ? GetComponent<SpriteRenderer> () : spriterenderer;
		if (spriterenderer == null) {
			return;
		}
		fromAlpha = Mathf.Clamp (fromAlpha, 0f, 1f);
		toAlpha = Mathf.Clamp (toAlpha, 0f, 1f);
		tempColor = spriterenderer.color;
		tempColor.a = reverse ? toAlpha : fromAlpha;
		spriterenderer.color = tempColor;
		durationTimer = duration;
		delayTimer = startDelay;
	}

	//再生開始
	public void Play(){
		enabled = true;
	}

	//停止
	public void Stop(){
		enabled = false;
	}

}
