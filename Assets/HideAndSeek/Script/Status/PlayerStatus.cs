using UnityEngine;
using System.Collections;

public class PlayerStatus : StatusBase {
	private PlayerHitPointBackgroundController playerHpBgController;

	protected override void Awake(){
		base.Awake ();
		base.hp = 100;
		base.attack = 10;
		playerHpBgController = GameObject.Find(PathHelper.HpEffectPath).GetComponent<PlayerHitPointBackgroundController>();
	}

	protected override void Update(){
		base.Update ();
		if(base.hp <= 0){
			Application.LoadLevel("Main");
		}
	}

	public void SubPlayerHitPoint(int damagePoint){
		base.hp -= damagePoint;
		playerHpBgController.AddAlphaValue(damagePoint);
	}
}
