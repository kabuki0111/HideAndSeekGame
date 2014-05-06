using UnityEngine;
using System.Collections;

public class SpotLightEnemyController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.gameManager).GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider other){
		if(other.name == "Player"){
			gameManager.isSearchPlayer = true;
			Debug.Log("okay!!   "+gameManager.isSearchPlayer);
		}
	}
}
