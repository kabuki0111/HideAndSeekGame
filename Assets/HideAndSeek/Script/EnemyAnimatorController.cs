using UnityEngine;
using System.Collections;

public class EnemyAnimatorController : MonoBehaviour {
	public int dyingState;
	public int locomotionState;
	public int deadBool;
	public int speedFloat;
	public int shotFloat;
	public int angularSpeedFloat;

	// Use this for initialization
	void Awake(){
		dyingState = Animator.StringToHash("Base Layer.Dying");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
		deadBool = Animator.StringToHash("Dead");
		speedFloat = Animator.StringToHash("Speed");
		shotFloat = Animator.StringToHash("Shot");
		angularSpeedFloat = Animator.StringToHash("AngularSpeed");
	}
}
