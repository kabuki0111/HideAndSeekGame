using UnityEngine;
using System.Collections;

public class PlayerHitPointBackgroundController : MonoBehaviour {
	private const float ALPHA_VALUE = 0.01f;		//alpha value

	private UIWidget uiWidgetDamageEffect;
	private UISprite uiHpSprite;

	private void Awake(){
		uiWidgetDamageEffect = this.gameObject.GetComponent<UIWidget>();
		uiHpSprite = this.gameObject.transform.FindChild(GameObjectNameHelper.UiPlayerHpSpriteName).GetComponent<UISprite>();
	}

	private void Start(){
		int left = -Mathf.Abs(Screen.width/2);
		int right = Mathf.Abs(Screen.width/2);
		int bottom = -Mathf.Abs(Screen.height/2);
		int top = Mathf.Abs(Screen.height/2);
		uiHpSprite.SetAnchor(uiHpSprite.gameObject, left, bottom, right, top);
	}

	public void DrawAlphaValue(int playerHitPoint){
		float ratioPlayerHp = playerHitPoint*ALPHA_VALUE;
		uiWidgetDamageEffect.alpha = Mathf.Abs(ratioPlayerHp - 1f);
	}
}
