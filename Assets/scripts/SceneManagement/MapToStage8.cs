using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]

public class MapToStage8: MonoBehaviour 
{
	protected void Start()
	{

		//Buttonクリック時、OnClickメソッドを呼び出す
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		//[Title]シーンに遷移
		SceneManager.LoadScene("Stage8");
	}
}