using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const string ATTACK_HASH = "1130333774";
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	private Animator anim;
	private SphereCollider attackColl;
	private AnimatorStateInfo stateInfo;
	private CharacterController characterController;
	private Vector3 axisTotalVector3;

	public float gravity = 20.0f;

	private void Awake(){
		anim = GetComponent<Animator>();
		attackColl = GameObject.Find(PathHelper.PlayerAttackColliderPath).GetComponent<SphereCollider>();
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		characterController = GetComponent<CharacterController>();
	}
	
	private void Update(){
		float axisHorizontalValue = Input.GetAxis("Horizontal") * MOVE_SPEED_ADJUSTMENT;
		float axisVerticalValue = Input.GetAxis("Vertical") * MOVE_SPEED_ADJUSTMENT;
		bool isPushKey = Input.anyKey;
		bool isSitDownKey = Input.GetKey(KeyCode.Space);

		MovementManagement(axisHorizontalValue, axisVerticalValue, isPushKey);
		AttackManagement();

		axisTotalVector3.y -= gravity*Time.deltaTime;
		characterController.Move(axisTotalVector3*Time.deltaTime);
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
				if(stateInfo.nameHash.ToString() == ATTACK_HASH){return;}
				axisTotalVector3 = new Vector3(horizontalValue, 0, verticalValue);
				transform.rotation = Quaternion.LookRotation(axisTotalVector3);
				axisTotalVector3 = transform.TransformDirection(axisTotalVector3);
				axisTotalVector3 *= MOVE_SPEED_ADJUSTMENT;
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
			stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			switch(stateInfo.nameHash.ToString()){
			case ATTACK_HASH:
				attackColl.enabled = true;
				anim.SetBool(AnimatorParametersHelper.PlayerParamAttackName, false);
				break;
			default:
				attackColl.enabled = false;
				break;
			}
		}
	}
}