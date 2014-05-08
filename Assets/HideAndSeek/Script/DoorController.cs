using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	public float toPositionX = 6f;
	public float toPositionY = 0;
	public float toPositionZ = 0;

	private Vector3 fromPosition;

	void Awake(){
		this.fromPosition = this.transform.position;
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == GameObjectNameHelper.PlayerObjectName || other.gameObject.tag == GameObjectNameHelper.EnemyTagName){
			Debug.Log("Door open!!");
			iTween.MoveTo(this.gameObject, iTween.Hash("x", fromPosition.x+toPositionX, "y", fromPosition.y+toPositionY, "z", fromPosition.z+toPositionZ, "time", 2.5f, "looptype", iTween.LoopType.none));
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.name==GameObjectNameHelper.PlayerObjectName || other.gameObject.tag==GameObjectNameHelper.EnemyTagName){
			Debug.Log("Door Close!!");
			iTween.MoveTo(this.gameObject, iTween.Hash("x", fromPosition.x, "y", fromPosition.y, "z", fromPosition.z, "time", 2.5f, "looptype", iTween.LoopType.none));
		}
	}
}
