using UnityEngine;
using System.Collections;

public enum DoorType{SMELL, MEDIUM, LARGE}; 

public class DoorController : MonoBehaviour {
	public DoorType doorType = DoorType.SMELL;
	public float toPositionX = 6f;
	public float toPositionY = 0;
	public float toPositionZ = 0;

	private Vector3 fromPosition;
	private Vector3 fromRightPosition;
	private Vector3 fromLeftPosition;

	void Awake(){
		switch(doorType){
		case DoorType.SMELL:
			this.fromPosition = this.transform.position;
			break;
		case DoorType.MEDIUM:
		case DoorType.LARGE:
			break;
		}
	}


	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == GameObjectNameHelper.PlayerObjectName || other.gameObject.tag == GameObjectNameHelper.EnemyTagName){
			switch(doorType){
			case DoorType.SMELL:
				iTween.MoveTo(this.gameObject, iTween.Hash("x", fromPosition.x+toPositionX, "y", fromPosition.y+toPositionY, "z", fromPosition.z+toPositionZ, "time", 2.5f, "looptype", iTween.LoopType.none));
				break;
			case DoorType.MEDIUM:
			case DoorType.LARGE:
				break;
			}
		}
	}


	void OnTriggerExit(Collider other){
		if(other.gameObject.name==GameObjectNameHelper.PlayerObjectName || other.gameObject.tag==GameObjectNameHelper.EnemyTagName){
			switch(doorType){
			case DoorType.SMELL:
				iTween.MoveTo(this.gameObject, iTween.Hash("x", fromPosition.x, "y", fromPosition.y, "z", fromPosition.z, "time", 2.5f, "looptype", iTween.LoopType.none));
				break;
			case DoorType.MEDIUM:
			case DoorType.LARGE:
				break;
			}
		}
	}

}
