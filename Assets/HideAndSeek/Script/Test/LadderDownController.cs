using UnityEngine;
using System.Collections;

public class LadderDownController : LadderControllerBase {
	public GameObject goalPosObject;

	protected override void Start(){
		base.Start ();
	}

	protected override void OnTriggerEnter(Collider other){
		base.OnTriggerEnter(other);
		switch(playerController.action){
		case playerAction.normal:
			playerController.action = playerAction.climb;
			break;
		case playerAction.climb:
			if(this.gameObject.name == "DownCollider"){
				Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>");
				playerController.gameObject.transform.position = goalPosObject.transform.position;
			}
			playerController.action = playerAction.normal;
			break;
		}
		other.gameObject.transform.position =
			new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
	}

	protected override void OnTriggerStay(Collider other){
		base.OnTriggerStay (other);
		if(!Input.GetKeyDown(KeyCode.M)){return;}
	}
}