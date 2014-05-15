using UnityEngine;
using System.Collections;

public class DebugScene : MonoBehaviour {
	private void Awake(){
		Application.LoadLevelAdditive("Main");
		Application.LoadLevelAdditive("Stage00");
	}
}
