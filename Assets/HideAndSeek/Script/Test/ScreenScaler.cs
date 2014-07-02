using UnityEngine;
using System.Collections;

public class ScreenScaler : MonoBehaviour {
	public GameObject baseObject;
	public bool aspectFit = true;
	
	private void Start(){
		Vector3 ratio = new Vector3(
			Screen.width / baseObject.transform.localScale.x,
			Screen.height / baseObject.transform.localScale.y,
			1.0f);
		
		if(aspectFit){
			if(ratio.x > ratio.y){
				ratio.x = ratio.y;
			}else if(ratio.y > ratio.x){
				ratio.y = ratio.x;
			}
		}
		
		transform.localScale = ratio;
	}
}
