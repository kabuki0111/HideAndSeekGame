using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == GameObjectTagHelper.EnemyTagName){
			Debug.Log("hit!! "+gameObject.name);
		}
	}
}