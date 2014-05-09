using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private static bool __isSearchPlayer;
	public bool isSearchPlayer{get; set;}

	void Awake(){
		isSearchPlayer = false;
		Debug.Log("game start isSearchPlayer ----> "+isSearchPlayer);
	}

}