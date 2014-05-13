using System.Collections;

static public class PathHelper{
	static private string __gameManagerPath = "GameController";
	static private string playerAttackColliderPath = "/Player/unitychan/Character1_Reference/Character1_Hips/Character1_LeftUpLeg/Character1_LeftLeg/Character1_LeftFoot";

	static public string gameManagerPath{
		get{return __gameManagerPath;}
	}
	static public string PlayerAttackColliderPath{
		get{return playerAttackColliderPath;}
	}
}
