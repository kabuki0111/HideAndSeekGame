using UnityEngine;
using System.Collections;

public class EnemyStatus : StatusBase {
	protected override void Awake(){
		base.Awake ();
		base.hp = 10;
		base.attack = 10;
		Debug.Log(string.Format("{0}  {1}  {2}", this.name, base.hp, base.attack));
	}
}
