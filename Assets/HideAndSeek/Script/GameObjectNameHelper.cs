using System.Collections;

static public class GameObjectNameHelper{
	static private string playerObjectName = "Player";
	static private string enemyTagName = "Enemy";

	static public string PlayerObjectName{
		get{return playerObjectName;}
	}
	static public string EnemyTagName{
		get{return enemyTagName;}
	}
}
