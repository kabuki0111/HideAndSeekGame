using UnityEngine;
using System.Collections;

public class OpenDoorController : MonoBehaviour {
	private GameManager gameManager;

	private void Awake(){
		gameManager = GameObject.Find(PathHelper.gameManagerPath).GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other){
		if(other.name != GameObjectNameHelper.playerObjectName){return;}
		gameManager.areaRoomDoorFlag = true;
		Destroy(this.gameObject);
	}
}
