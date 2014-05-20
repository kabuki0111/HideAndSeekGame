using UnityEngine;
using UnityEditor;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	private Vector3 posTerrain;
	private Vector3 posWater;

	private void Awake(){
	}

	private void OnTriggerEnter(Collider other){
		if(other.tag != GameObjectTagHelper.playerTagName){return;}

		if(GameObject.Find(GameObjectTagHelper.backgroundTagName) != null){
			GameObject obj = GameObject.Find(GameObjectTagHelper.backgroundTagName);
			EditorApplication.delayCall += () => DestroyImmediate(obj);
		}else{
			Instantiate(Resources.Load("Backgrounds"), posTerrain, new Quaternion(0, 0, 0, 0));
		}
	}
}
