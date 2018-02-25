using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	//DayTime:working time  ,  starttime:start working time
	public float DayTime = 10; //hour :~時間動けるか
	public float starttime = 8; //hour :~時から動くか
	public float GameSpeed; //現実時間との倍率(exp: 2 → 1scに2sc進む)

	private float day = 1;
	private float second = 0;
	private float minute = 0;
	private float hour = 0;


	// Use this for initialization
	void Start () {

		hour = starttime;

		GetComponent<Text> ().text = day.ToString () + "日目" + hour.ToString () + "時" + minute.ToString () + "分";
	}
	
	// Update is called once per frame
	void Update () {
		

		second += (Time.deltaTime) * GameSpeed;

		if (second >= 60) {
			minute += 1;
			second = 0;
		}

		if (minute >= 60) {
			hour += 1;
			minute = 0;
		}

		if (hour >= DayTime + starttime) {
			day += 1;
			hour = starttime;
		}
		GetComponent<Text> ().text = day.ToString() + "日目" + hour.ToString () + "時" + minute.ToString () + "分" + ((int)second).ToString () + "秒";
	}
}
