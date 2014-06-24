using UnityEngine;
using System.Collections;

public enum  playerAction{
	normal,
	climb
}

public class PlayerController : MonoBehaviour {
	private const string ATTACK_HASH = "1130333774";
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	private Animator anim;
	private SphereCollider attackColl;
	private AnimatorStateInfo stateInfo;
	private CharacterController characterController;
	private Vector3 axisTotalVector3;

	public playerAction action;
	public float gravity = 20.0f;

	private void Awake(){
		action = playerAction.normal;
		anim = GetComponent<Animator>();
		attackColl = GameObject.Find(PathHelper.playerAttackColliderPath).GetComponent<SphereCollider>();
		stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		characterController = GetComponent<CharacterController>();
	}
	
	private void Update(){
		Debug.Log("character -->"+ characterController.isGrounded);
		//if(!characterController.isGrounded){
			if(anim.GetBool(AnimatorParametersHelper.playerParamRunName)){
				anim.SetBool(AnimatorParametersHelper.playerParamRunName, false);
			}
			//return;
		//}

		bool isPushKey = Input.anyKey;

		switch(action){
		case playerAction.normal:
			MovementManagement(isPushKey);
			AttackManagement();
			axisTotalVector3.y -= gravity*Time.deltaTime;
			characterController.Move(axisTotalVector3*Time.deltaTime);
			break;
		case playerAction.climb:
			ClimbManagement(isPushKey);
			break;
		}
	}

	//player move func
	private void MovementManagement(bool isKey){
		if(isKey){
			string currentPushKeyName = Input.inputString;
			Debug.Log("hoge --->"+currentPushKeyName);
	
			if(
				Input.GetKey(KeyCode.A) ||
			    Input.GetKey(KeyCode.D) ||
			    Input.GetKey(KeyCode.W) ||
			    Input.GetKey(KeyCode.S) ||
			    Input.GetKey(KeyCode.UpArrow) ||
			    Input.GetKey(KeyCode.DownArrow) ||
			    Input.GetKey(KeyCode.RightArrow) ||
			    Input.GetKey(KeyCode.LeftArrow)
			){
				if(stateInfo.nameHash.ToString() == ATTACK_HASH){return;}
				Debug.Log("push key now!!");
				Vector3 forward = Camera.mainCamera.transform.TransformDirection(Vector3.forward);
				Vector3 right = Camera.mainCamera.transform.TransformDirection(Vector3.right);
				axisTotalVector3 = Input.GetAxis("Horizontal")*right + Input.GetAxis("Vertical")*forward;
				axisTotalVector3.y = 0;
				transform.rotation = Quaternion.LookRotation(axisTotalVector3);
				axisTotalVector3 *= MOVE_SPEED_ADJUSTMENT;
				anim.SetBool(AnimatorParametersHelper.playerParamRunName, true);
			}
		}else{
			anim.SetBool(AnimatorParametersHelper.playerParamRunName, false);
		}
	}

	//player attack func
	private void AttackManagement(){
		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool(AnimatorParametersHelper.playerParamAttackName, true);
		}else{
			stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			switch(stateInfo.nameHash.ToString()){
			case ATTACK_HASH:
				attackColl.enabled = true;
				anim.SetBool(AnimatorParametersHelper.playerParamAttackName, false);
				break;
			default:
				attackColl.enabled = false;
				break;
			}
		}
	}

	//player climb func
	private void ClimbManagement(bool isKey){
		if(this.GetComponent<CharacterController>().enabled){
			Debug.Log(">>>> false");
			this.GetComponent<CharacterController>().enabled = false;
		}

		if(isKey){
			string currentPushKeyName = Input.inputString;
			switch(currentPushKeyName){
			case "w":
				transform.Translate(0, 3f, 0);
				break;
			case "s":
				transform.Translate(0, -3f, 0);
				break;
			}
		}
	}
}