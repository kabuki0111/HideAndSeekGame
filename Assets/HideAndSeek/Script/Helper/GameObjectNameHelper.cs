using System.Collections;

static public class GameObjectNameHelper{
	static private string playerObjectName = "Player";
	static private string enemyObjectName = "Enemy";
	static private string robotGuardName = "char_robotGuard";
	static private string uiPlayerHpSpriteName = "HpSprite";

	static public string PlayerObjectName{
		get{return playerObjectName;}
	}
	static public string EnemyObjectName{
		get{return enemyObjectName;}
	}
	static public string RobotGuardName{
		get{return robotGuardName;}
	}
	static public string UiPlayerHpSpriteName{
		get{return uiPlayerHpSpriteName;}
	}
}