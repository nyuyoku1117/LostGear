using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject player = null;
	private Vector3 offset = Vector3.zero;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		if(transform.position.y>(-1)*10f){
			Vector3 newPosition = transform.position;
			newPosition.x = player.transform.position.x + offset.x;
			newPosition.y = player.transform.position.y + offset.y;
			newPosition.z = player.transform.position.z + offset.z;
			transform.position = Vector3.Lerp(transform.position,newPosition,5.0f * Time.deltaTime);
		}else{
			Vector3 newPosition = transform.position;
			transform.position = Vector3.Lerp(transform.position,newPosition,5.0f * Time.deltaTime);
		}
	}
}
