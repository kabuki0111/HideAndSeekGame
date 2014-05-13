using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour {
	private PlayerStatus playerStatus;

	void Awake(){
		playerStatus = GameObject.Find(GameObjectNameHelper.PlayerObjectName).GetComponent<PlayerStatus>();
		Debug.Log("----> "+playerStatus);
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != GameObjectTagHelper.DamageRegionTagName){return;}
		Debug.Log("hit!! "+other.gameObject.name);
		EnemyStatus enemyStatus = other.gameObject.GetComponent<EnemyStatus>();
		enemyStatus.hp -= playerStatus.attack;
	}
}