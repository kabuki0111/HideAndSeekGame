using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	private Vector3 fromPosition;
	private Vector3 toPosition;

	void Awake(){
		fromPosition = transform.position;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Player"){
			Debug.Log("Door open!!");
			iTween.MoveTo(gameObject, iTween.Hash("x", fromPosition.x+6f, "time", 2.5f, "looptype", iTween.LoopType.none));
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.name == "Player"){
			Debug.Log("Door Close!!");
			iTween.MoveTo(gameObject, iTween.Hash("x", fromPosition.x, "time", 2.5f, "looptype", iTween.LoopType.none));
		}
	}
}
