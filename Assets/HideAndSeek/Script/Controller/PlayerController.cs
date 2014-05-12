using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	private int __hpPlayer;
	private Animator anim;
	private SphereCollider attackColl;
	private AnimatorStateInfo stateInfo;

	public int hpPlayer{get; set;}

	void Awake(){
		anim = GetComponent<Animator>();
		attackColl = GameObject.Find(PathHelper.PlayerAttackColliderPath).GetComponent<SphereCollider>();
		hpPlayer = 100;
	}
	
	void Update(){
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		float axisHorizontalValue = Input.GetAxis("Horizontal") * MOVE_SPEED_ADJUSTMENT;
		float axisVerticalValue = Input.GetAxis("Vertical") * MOVE_SPEED_ADJUSTMENT;
		bool isPushKey = Input.anyKey;
		bool isSitDownKey = Input.GetKey(KeyCode.Space);
		MovementManagement(axisHorizontalValue, axisVerticalValue, isPushKey);
		AttackManagement();
	}

	//player attack func
	private void MovementManagement(float horizontalValue, float verticalValue, bool isKey){
		if(isKey){
			string currentPushKeyName = Input.inputString;
			switch(currentPushKeyName){
			case "a":
			case "w":
			case "s":
			case "d":
				if(stateInfo.nameHash.ToString() == "1130333774"){return;}
				Vector3 axisTotalVector3 = new Vector3(horizontalValue, 0, verticalValue);
				transform.rotation = Quaternion.LookRotation(axisTotalVector3);
				transform.position += axisTotalVector3;
				anim.SetBool(AnimatorParametersHelper.PlayerParamRunName, true);
				break;
			}
		}else{
			anim.SetBool(AnimatorParametersHelper.PlayerParamRunName, false);
		}
	}

	//player attack func
	private void AttackManagement(){
		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool(AnimatorParametersHelper.PlayerParamAttackName, true);
		}else{
			if(stateInfo.nameHash.ToString() != "1130333774"){
				attackColl.enabled = false;
				anim.SetBool(AnimatorParametersHelper.PlayerParamAttackName, false);
			}else{
				attackColl.enabled = true;
			}
		}
	}


}