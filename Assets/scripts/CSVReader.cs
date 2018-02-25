using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class CSVReader : MonoBehaviour{


	/*
	ゲームデータ読み込み(ロード)時に利用できるか？
	セーブの際はまた別のスクリプトを利用して書き出す(上書き)
	 */



	public TextAsset csvFile;
	List<string[]> csvDatas = new List<string[]>();

	void Start(){

		//格納
		string[] lines = csvFile.text.Replace("\r\n" , "\n").Split("\n"[0]);

		foreach (var line in lines) {

			if (line == "") {
				continue;
			}
			csvDatas.Add (line.Split (','));

		}

		Debug.Log (csvDatas.Count); //行の数
		Debug.Log (csvDatas [0].Length); //列の数
		Debug.Log (csvDatas [2] [1]); //3行目2列

	}


}