using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.GameManagerPath).GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.name != GameObjectNameHelper.PlayerObjectName) return;
		if(gameManager.isSearchPlayer) return;
		Debug.Log("okay");
		iTween.MoveTo(gameObject, iTween.Hash("y", 24f, "time", 3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.none));
	}
}
