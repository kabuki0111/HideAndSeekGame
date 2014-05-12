using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHelper.gameManagerPath).GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.name != GameObjectNameHelper.PlayerObjectName) return;
		if(gameManager.isSearchPlayer) return;
		Debug.Log("okay");
		//Application.LoadLevel("Main");
	}
}
