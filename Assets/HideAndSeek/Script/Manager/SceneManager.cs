﻿using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public bool isFlag = true;

	private void Awake(){
		if(!isFlag){return;}
		Application.LoadLevelAdditive(SceneNameHelper.stage01Name);
		Application.LoadLevelAdditive(SceneNameHelper.stage00Name);
		Application.LoadLevelAdditive(SceneNameHelper.selectName);
	}
}
