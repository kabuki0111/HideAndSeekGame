using UnityEngine;
using System.Collections;

static public class GameObjectTagHelper{
	static private string playerTagName = "Player";
	static private string enemyTagName = "Enemy";

	
	static public string PlayerTagName{
		get{return playerTagName;}
	}
	static public string EnemyTagName{
		get{return enemyTagName;}
	}
}
