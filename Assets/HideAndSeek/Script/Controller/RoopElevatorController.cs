using UnityEngine;
using System.Collections;


public enum MoveType{
	UPER,
	DOWN
}

public class RoopElevatorController : ElevatorController {
	public float uperGoalVecY = 23.3f;
	public float downGoalVecY = -67f;

	public MoveType moveType;

	protected override void Awake(){
		Debug.Log("move type ----> "+moveType);
		base.Awake ();
	}

	private void Update(){
		if(moveType == MoveType.DOWN){
			iTween.MoveUpdate(this.gameObject, iTween.Hash("y", downGoalVecY, "time", 8f));
		}else if(moveType == MoveType.UPER){
			iTween.MoveUpdate(this.gameObject, iTween.Hash("y", uperGoalVecY, "time", 8f));
		}
	}

	protected override void OnTriggerEnter (Collider other){
		if(other.name != GameObjectNameHelper.playerObjectName){return;}
		other.transform.parent = gameObject.transform;

		if(this.transform.position.y >= uperGoalVecY){
			moveType = MoveType.DOWN;
		}else if(this.transform.position.y <= 0){
			moveType = MoveType.UPER;
		}
		Debug.Log("move type ----> "+moveType);
	}

	private void OnTriggerStay(Collider other){
		if(other.name != GameObjectNameHelper.playerObjectName){return;}
		if(moveType == MoveType.DOWN){
			if(this.transform.position.y > downGoalVecY+10){return;}
			other.transform.parent = null;
		}else if(moveType == MoveType.UPER){
			if(this.transform.position.y < uperGoalVecY-10){return;}
			other.transform.parent = null;
		}
	}
}
