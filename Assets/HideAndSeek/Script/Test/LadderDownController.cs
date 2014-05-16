using UnityEngine;
using System.Collections;

public class LadderDownController : LadderControllerBase {

	protected override void OnTriggerStay(Collider other){
		base.OnTriggerStay (other);
		if(!Input.GetKeyDown(KeyCode.Space)){return;}
		other.gameObject.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
	}
}
