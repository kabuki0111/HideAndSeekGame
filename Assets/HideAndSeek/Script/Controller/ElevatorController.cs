using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	GameManager gameManager;
	GameObject playerObject;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.GameManagerPath).GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.name != GameObjectNameHelper.PlayerObjectName) return;
		if(gameManager.isSearchPlayer) return;
		playerObject = other.gameObject;
		playerObject.transform.parent = gameObject.transform;
		iTween.MoveTo(gameObject, iTween.Hash("y", 24f, "time", 3f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "CompleteHandler", "looptype", iTween.LoopType.none));
	}

	private void CompleteHandler(){
		playerObject.transform.parent = null;
	}
}
