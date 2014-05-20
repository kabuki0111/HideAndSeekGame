using UnityEngine;
using System.Collections;

public class LadderControllerBase : MonoBehaviour {

	protected virtual void OnTriggerEnter(Collider other){
		if(other.name != GameObjectTagHelper.playerTagName){return;}
		Debug.Log("player down now.");
	}
	
	protected virtual void OnTriggerStay(Collider other){
		if(other.name != GameObjectTagHelper.playerTagName){return;}
		Debug.Log("down start!!");
	}
}
