using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static bool __isSearchPlayer;
	public bool isSearchPlayer{get; set;}

	private PlayerController playerController;

	void Awake(){
		isSearchPlayer = false;
		playerController = GameObject.Find(GameObjectNameHelper.PlayerObjectName).GetComponent<PlayerController>();
	}
}