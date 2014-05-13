using UnityEngine;
using System.Collections;

static public class GameObjectTagHelper{
	static private string playerTagName = "Player";
	static private string enemyTagName = "Enemy";
	static private string playerKickTagName = "Kick";
	static private string damageRegionTagName = "DamageRegionPoint";

	static public string PlayerTagName{
		get{return playerTagName;}
	}
	static public string EnemyTagName{
		get{return enemyTagName;}
	}
	static public string PlayerKickTagName{
		get{return playerKickTagName;}
	}
	static public string DamageRegionTagName{
		get{return damageRegionTagName;}
	}
}
