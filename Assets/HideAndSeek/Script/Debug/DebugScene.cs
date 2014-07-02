using UnityEngine;
using System.Collections;

public enum SceneName{
	main,
	stage00,
	stage01
}

public class DebugScene : MonoBehaviour {
	public bool isDebugPlay;
	public SceneName sceneName;

	private void Awake(){
		if(!isDebugPlay){return;}

		if(sceneName != SceneName.stage01){
			Application.LoadLevelAdditive(SceneNameHelper.stage01Name);
		}
		
		if(sceneName != SceneName.stage00){
			Application.LoadLevelAdditive(SceneNameHelper.stage00Name);
		}

		if(sceneName != SceneName.main){
			Application.LoadLevelAdditive(SceneNameHelper.mainName);
		}

	}
}
