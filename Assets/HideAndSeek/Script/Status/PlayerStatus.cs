using UnityEngine;
using System.Collections;

public class PlayerStatus : StatusBase {
	private PlayerHitPointBackgroundController playerHpBgController;
	private int maxPlayerHp;
	private int recovePlayerHpValue;
	private float startRecovePlayerHpTimer = 5f;
	private float countTimer = 0;

	public int recovaOnePoint = 2;

	protected override void Awake(){
		base.Awake ();
		base.hp = 100;
		maxPlayerHp = base.hp;
		base.attack = 10;
		playerHpBgController = GameObject.Find(PathHelper.hpSpritePath).GetComponent<PlayerHitPointBackgroundController>();
	}

	protected override void Update(){
		base.Update ();

		if(base.hp <= 0){
			Application.LoadLevel("Main");
		}

		if(base.hp != maxPlayerHp){
			countTimer += Time.deltaTime;
			if(countTimer < startRecovePlayerHpTimer){return;}
			RecovePlayerHitPoint();
		}
	}

	private void RecovePlayerHitPoint(){
		base.hp += recovaOnePoint;
		playerHpBgController.DrawAlphaValue(base.hp);
	}

	public void DamagePlayerHitPoint(int damagePoint){
		base.hp -= damagePoint;
		countTimer = 0;
		playerHpBgController.DrawAlphaValue(base.hp);
	}
}
