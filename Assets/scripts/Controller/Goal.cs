using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class Goal : MonoBehaviour {

	//private GameObject player;
	public bool isGoal = false;
	public bool isShowed = false;
	private Coroutine coroutine;
	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {
		if (isGoal) {
			if (isShowed == false) {
				if (FindObjectOfType<EventSystem> () == null) {
					var es = new GameObject ("EventSystem", typeof(EventSystem));
					es.AddComponent<StandaloneInputModule> ();
				}
				/*PlayerControler pp = player.GetComponent<PlayerControler>();
				pp.anim.speed = 0;
				*/
				var canvasObject = new GameObject ("Canvas");
				var canvas = canvasObject.AddComponent<Canvas> ();
				canvasObject.AddComponent<GraphicRaycaster> ();
				canvas.renderMode = RenderMode.ScreenSpaceOverlay;

				var textObject = new GameObject ("Text");
				textObject.transform.parent = canvas.transform;
				var text = textObject.AddComponent<Text> ();
				text.rectTransform.sizeDelta = Vector2.zero;
				text.rectTransform.anchorMin = Vector2.zero;
				text.rectTransform.anchorMax = Vector2.one;
				text.rectTransform.anchoredPosition = new Vector2 (.5f, .5f);
				text.text = "GAMECLEAR!";
				text.font = Resources.FindObjectsOfTypeAll<Font> () [0];
				text.fontSize = 20;
				text.color = Color.yellow;
				text.alignment = TextAnchor.MiddleCenter;
		
				isShowed = true;


				if (coroutine == null) {
					//3.5秒後に実行する
					coroutine = StartCoroutine ("DelayMethod");
				}

				/// <summary>
				/// 渡された処理を指定時間後に実行する
				/// </summary>
				/// <param name="waitTime">遅延時間[ミリ秒]</param>
				/// <param name="action">実行したい処理</param>
				/// <returns></returns>
				///
			



			}
		}
	}
	void OnTriggerEnter2D(Collider2D col){
	
		if (col.gameObject.tag == "Player") {
			isGoal = true;
		}
	}
	private IEnumerator DelayMethod()
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Scenes/Title");
	}
}

