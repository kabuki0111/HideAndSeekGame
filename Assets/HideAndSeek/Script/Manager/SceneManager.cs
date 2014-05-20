using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public bool isFlag = true;

	private void Awake(){
		if(isFlag){
			Application.LoadLevelAdditive("Stage00");
			Application.LoadLevelAdditive("Stage01");
		}
	}
}
