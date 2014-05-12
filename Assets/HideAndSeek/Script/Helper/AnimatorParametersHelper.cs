using System.Collections;

static public class AnimatorParametersHelper {
	static private string playerParamRunName = "isRun";
	static private string playerParamAttackName = "isAttack";
	static private string playerParamIdleName = "isIdle";

	static public string PlayerParamRunName{
		get{return playerParamRunName;}
	}
	static public string PlayerParamAttackName{
		get{return playerParamAttackName;}
	}
	static public string PlayerParamIdleName{
		get{return playerParamIdleName;}
	}
}
