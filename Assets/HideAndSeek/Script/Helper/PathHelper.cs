using System.Collections;

static public class PathHelper{
	static private string gameManagerPath = "GameController";
	static private string playerAttackColliderPath = "/Player/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg/Character1_LeftFoot";
	static private string hpSpritePath = "/UI Root/UIWidgetHitEffect";

	static public string GameManagerPath{
		get{return gameManagerPath;}
	}
	static public string PlayerAttackColliderPath{
		get{return playerAttackColliderPath;}
	}
	static public string HpEffectPath{
		get{return hpSpritePath;}
	}
}
