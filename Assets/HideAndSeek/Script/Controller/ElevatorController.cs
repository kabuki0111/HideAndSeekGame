using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	protected GameManager gameManager;
	protected GameObject playerObject;

	protected virtual void Awake(){
		gameManager = GameObject.Find(PathHelper.gameManagerPath).GetComponent<GameManager>();
	}

	protected virtual void OnTriggerEnter(Collider other){
		if(other.name != GameObjectNameHelper.playerObjectName){return;}
		if(gameManager.isSearchPlayer){return;}
		playerObject = other.gameObject;
		playerObject.transform.parent = gameObject.transform;
		iTween.MoveTo(gameObject, iTween.Hash("y", 24f, "time", 3f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "CompleteHandler", "looptype", iTween.LoopType.none));
	}

	protected virtual void CompleteHandler(){
		playerObject.transform.parent = null;
	}
}
