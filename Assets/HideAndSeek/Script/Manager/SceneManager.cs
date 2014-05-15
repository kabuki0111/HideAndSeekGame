using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	void Awake(){
		Application.LoadLevelAdditive("Stage00");
	}
}
