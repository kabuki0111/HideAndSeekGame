using UnityEngine;
using System.Collections;

public class EnemyStatus : StatusBase {

	protected override void Awake(){
		base.Awake ();
		base.hp = 10;
		base.attack = 10;
		Debug.Log(string.Format("{0}  {1}  {2}", this.name, base.hp, base.attack));
	}

	protected override void Update(){
		base.Update ();
		if(base.hp <= 0){
			Debug.Log(this.gameObject.name+" destroy!!");
		}
	}
}
