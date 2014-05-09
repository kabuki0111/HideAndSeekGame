using UnityEngine;
using System.Collections;

public class SpotLightEnemyController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.gameManager).GetComponent<GameManager>();
		iTween.MoveTo(gameObject, iTween.Hash("x", -12, "time", 6f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void OnTriggerEnter(Collider other){
		if(other.name == PathHelper.gameManager){
			gameManager.isSearchPlayer = true;
			Debug.Log("okay!!   "+gameManager.isSearchPlayer);
		}
	}
}
