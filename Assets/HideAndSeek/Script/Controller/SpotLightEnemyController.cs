using UnityEngine;
using System.Collections;

public class SpotLightEnemyController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.GameManagerPath).GetComponent<GameManager>();
		iTween.MoveTo(gameObject, iTween.Hash("x", -12, "time", 6f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == GameObjectTagHelper.PlayerTagName){
			gameManager.isSearchPlayer = true;
			Debug.Log("okay!!   "+gameManager.isSearchPlayer);
		}
	}
}
