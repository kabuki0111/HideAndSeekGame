using UnityEngine;
using System.Collections;

public class PlayerHitPointBackgroundController : MonoBehaviour {
	private const float ALPHA_VALUE = 0.01f;		//alpha value

	private UIWidget uiWidgetDamageEffect;

	private void Awake(){
		uiWidgetDamageEffect = this.gameObject.GetComponent<UIWidget>();
	}

	public void DrawAlphaValue(int playerHitPoint){
		float ratioPlayerHp = playerHitPoint*ALPHA_VALUE;
		uiWidgetDamageEffect.alpha = Mathf.Abs(ratioPlayerHp - 1f);

	}
}
