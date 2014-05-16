using UnityEngine;
using System.Collections;

public class LadderControllerBase : MonoBehaviour {

	protected virtual void OnTriggerEnter(Collider other){
		if(other.name != GameObjectTagHelper.PlayerTagName){return;}
		Debug.Log("player down now.");
	}
	
	protected virtual void OnTriggerStay(Collider other){
		if(other.name != GameObjectTagHelper.PlayerTagName){return;}
		Debug.Log("down start!!");
	}
}
