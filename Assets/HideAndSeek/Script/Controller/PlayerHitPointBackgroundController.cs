using UnityEngine;
using System.Collections;

public class PlayerHitPointBackgroundController : MonoBehaviour {
	private const float ALPHA_VALUE = 0.01f;		//alpha value

	private UIWidget uiWidgetDamageEffect;

	private void Awake(){
		uiWidgetDamageEffect = this.gameObject.GetComponent<UIWidget>();
	}

	public void AddAlphaValue(int damagePoint){
		float drowAlphaValue = damagePoint*ALPHA_VALUE;
		uiWidgetDamageEffect.alpha += drowAlphaValue;
	}
}
