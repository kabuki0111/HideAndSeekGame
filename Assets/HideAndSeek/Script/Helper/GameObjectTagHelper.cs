using UnityEngine;
using System.Collections;

static public class GameObjectTagHelper{
	static private string playerTagName = "Player";
	static private string enemyTagName = "Enemy";
	static private string playerKickTagName = "Kick";
	static private string damageRegionTagName = "DamageRegionPoint";
	static private string terrainTagName = "Terrain";
	static private string waterTagName = "Water";
	static private string backgroundTagName = "Backgrounds";

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
	static public string TerrainTagName{
		get{return terrainTagName;}
	}
	static public string WaterTagName{
		get{return waterTagName;}
	}
	static public string BackgroundTagName{
		get{return backgroundTagName;}
	}
}
