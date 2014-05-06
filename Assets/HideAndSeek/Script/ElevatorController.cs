using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find(PathHepler.gameManager).GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.name != "Player") return;
		if(!gameManager.isSearchPlayer){
			Debug.Log("okay");
			Application.LoadLevel("Main");
		}
	}
}
