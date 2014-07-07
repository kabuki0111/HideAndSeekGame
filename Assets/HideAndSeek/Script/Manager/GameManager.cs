using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static bool __isSearchPlayer;
	public bool isSearchPlayer{get; set;}

	private bool __areaRoomDoorFlag;			//ロックの掛かったドアを空けるフラグ.
	public bool areaRoomDoorFlag{get; set;}

	private PlayerController playerController;
	
	void Awake(){
		isSearchPlayer = false;
		areaRoomDoorFlag = false;
		playerController = GameObject.Find(GameObjectNameHelper.playerObjectName).GetComponent<PlayerController>();
	}
}