using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static bool __isSearchPlayer;
	public bool isSearchPlayer{get; set;}

	private PlayerController playerController;

	void Awake(){
		isSearchPlayer = false;
		Debug.Log("game start isSearchPlayer ----> "+isSearchPlayer);
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	void Update(){
		if(playerController.hpPlayer <= 0){
			Application.LoadLevel("Main");
		}
	}

}