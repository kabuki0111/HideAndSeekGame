using UnityEngine;
using System.Collections;

public class EnemyStatus : StatusBase {

	protected override void Awake(){
		base.Awake ();
		base.hp = 10;
		base.attack = 10;
	}

	protected override void Update(){
		base.Update ();
		if(base.hp <= 0){
			Debug.Log(this.gameObject.name+" destroy!!");
		}
	}
}
