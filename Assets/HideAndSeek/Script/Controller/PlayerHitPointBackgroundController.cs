using UnityEngine;
using System.Collections;

public class PlayerHitPointBackgroundController : MonoBehaviour {
	private const float ALPHA_VALUE = 0.01f;		//alpha value

	private UIWidget uiWidgetDamageEffect;
	private UISprite uiHpSprite;

	private void Awake(){
		uiWidgetDamageEffect = this.gameObject.GetComponent<UIWidget>();
		uiHpSprite = this.gameObject.transform.FindChild(GameObjectNameHelper.uiPlayerHpSpriteName).GetComponent<UISprite>();
	}

	private void Start(){
		int halfWidth = Screen.width/2;
		int halfHeight = Screen.height/2;

		int left = -Mathf.Abs(halfWidth);
		int right = Mathf.Abs(halfWidth);
		int bottom = -Mathf.Abs(halfHeight);
		int top = Mathf.Abs(halfHeight);
		uiHpSprite.SetAnchor(uiHpSprite.gameObject, left, bottom, right, top);
	}

	public void DrawAlphaValue(int playerHitPoint){
		float ratioPlayerHp = playerHitPoint*ALPHA_VALUE;
		uiWidgetDamageEffect.alpha = Mathf.Abs(ratioPlayerHp - 1f);
	}
}
