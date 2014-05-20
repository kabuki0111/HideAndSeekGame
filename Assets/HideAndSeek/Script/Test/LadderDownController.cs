using UnityEngine;
using System.Collections;

public class LadderDownController : LadderControllerBase {

	protected override void OnTriggerStay(Collider other){
		base.OnTriggerStay (other);
		if(!Input.GetKeyDown(KeyCode.M)){return;}
		//other.GetComponent<PlayerController>().action = playerAction.climb;
		other.gameObject.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
	}
}
