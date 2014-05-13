using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != GameObjectTagHelper.DamageRegionTagName){return;}
		Debug.Log("hit!! "+other.gameObject.name);
	}
}