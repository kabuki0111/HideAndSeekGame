using UnityEngine;
using System.Collections;

public class RockDoorController : DoorController {
	private GameObject rockOffObject;

	private void Awake(){
		base.Awake();
	}

	protected override void OnTriggerEnter(Collider other){
		if(!gameManager.areaRoomDoorFlag){return;}
		base.OnTriggerEnter(other);
	}

	protected override void OnTriggerExit(Collider other){
		if(!gameManager.areaRoomDoorFlag){return;}
		base.OnTriggerExit(other);
	}
}
