using UnityEngine;
using System.Collections;

public class PlayerStatus : StatusBase {
	protected override void Awake(){
		base.Awake ();
		base.hp = 100;
		base.attack = 10;
		Debug.Log(string.Format("{0}  {1}  {2}", this.name, base.hp, base.attack));
	}

	protected override void Update(){
		base.Update ();
		if(base.hp <= 0){
			Application.LoadLevel("Main");
		}
	}
}
