using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {
	private PlayerStatus playerStatus;

	void Awake(){
		playerStatus = GameObject.Find(GameObjectNameHelper.playerObjectName).GetComponent<PlayerStatus>();
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != GameObjectTagHelper.damageRegionTagName){return;}
		Debug.Log("hit!! "+other.gameObject.name);
		EnemyStatus enemyStatus = other.gameObject.GetComponent<EnemyStatus>();
		enemyStatus.hp -= playerStatus.attack;
	}
}