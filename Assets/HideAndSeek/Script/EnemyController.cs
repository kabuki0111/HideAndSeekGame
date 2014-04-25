using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public Transform[] wayPointIndex;
	public float fieldOfViewAngle = 110f;
	public float walkSpeed = 0.3f;
	public float dashSpeed = 4.75f;
	public float attackRange = 200f;

	private NavMeshAgent navAgent;
	private Animator animtor;
	private EnemyAnimatorController animatorController;
	private SphereCollider opticSphereCol;
	private GameObject playerGameObject;
	private	int patrolIndex;
	private float chaseTimer;
	private Vector3 targetPosition;	
	private bool isShooting;
	private bool isChaseToPlayer;
	private bool isLostToPlayer;

	void Awake(){
		animtor = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		opticSphereCol = GetComponent<SphereCollider>();
		playerGameObject = GameObject.FindGameObjectWithTag(DoneTags.player);
		animatorController = GameObject.Find("GameController").GetComponent<EnemyAnimatorController>();

		animtor.SetLayerWeight(1, 1f);
		animtor.SetLayerWeight(2, 1f);
	}

	void Update(){
		if(isChaseToPlayer){
			Chase();
		}else{
			Patrol();
		}
	}

	void OnTriggerStay(Collider other){
		OutLook(other);
	}

	void OnTriggerExit(){
	}

	void OnAnimatorIK(int layerIndex){
		float aimWeight = animtor.GetFloat(animatorController.aimWeightFloat);
		animtor.SetIKPosition(AvatarIKGoal.RightHand, playerGameObject.transform.position + Vector3.up * 1.5f);
		animtor.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}

	//プレイヤーを追跡するメソッド.
	private void Chase(){
		float angle = FindAngle(transform.forward, playerGameObject.transform.position-transform.position, transform.up);

		Vector3 sightingDeltaPos = playerGameObject.transform.position - transform.position;
		if(sightingDeltaPos.sqrMagnitude <attackRange){
			animtor.SetBool(animatorController.shoutingBool, true);
			navAgent.Stop();
			animtor.SetFloat(animatorController.angularSpeedFloat, 0);
			animtor.SetFloat(animatorController.speedFloat, dashSpeed);
		}else{
			if(isShooting){
				isShooting = false;
				chaseTimer = 0;
				targetPosition = playerGameObject.transform.position - transform.position;
				animtor.SetFloat(animatorController.angularSpeedFloat, angle);
				animtor.SetFloat(animatorController.speedFloat, dashSpeed);
				navAgent.SetDestination(targetPosition);
			}
		}

		if(!isShooting){
			chaseTimer += Time.deltaTime;
			if(chaseTimer>=2f){
				isShooting = true;
			}
		}
	}

	//エネミーの巡回メソッド.
	private void Patrol(){
		if(navAgent.remainingDistance < navAgent.stoppingDistance){
			navAgent.SetDestination(wayPointIndex[patrolIndex].position);
			float angle = FindAngle(transform.forward, wayPointIndex[patrolIndex].position-transform.position, transform.up);
			animtor.SetFloat(animatorController.angularSpeedFloat, angle);
			animtor.SetFloat(animatorController.speedFloat, walkSpeed);
			int targetLength = wayPointIndex.Length-1;
			patrolIndex = patrolIndex>=targetLength ? 0 : patrolIndex+1;
		}
	}

	//エネミーの視界メソッド.
	private void  OutLook(Collider other){
		if(other.gameObject == playerGameObject){
			Vector3 direction = other.transform.position - transform.position;
			float	angle = Vector3.Angle(direction, transform.forward);

			//if(angle >= fieldOfViewAngle*0.5){return;}
			if(angle >= fieldOfViewAngle*0.5){
				RaycastHit hit;
				int layerMask = 1<<10;
				bool isFindPlayer = Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, opticSphereCol.radius, layerMask);

				if(!isFindPlayer){return;}

				animtor.SetBool(animatorController.Chase, true);
				isChaseToPlayer = true;
			}else{
				if(!isChaseToPlayer){return;}
				Debug.Log("----->");
				LostToPlayer();
			}
		}
	}

	private float endChaseTimer = 0;
	private void LostToPlayer(){
		endChaseTimer += Time.deltaTime;
		if(endChaseTimer < 5f){return;}
		isChaseToPlayer = false;
		patrolIndex = 0;
		navAgent.SetDestination(wayPointIndex[patrolIndex].position);
		float angle = FindAngle(transform.forward, wayPointIndex[patrolIndex].position-transform.position, transform.up);
		animtor.SetFloat(animatorController.angularSpeedFloat, angle);
		animtor.SetFloat(animatorController.speedFloat, walkSpeed);
		animtor.SetBool(animatorController.shoutingBool, false);
		endChaseTimer = 0;
	}

	private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector){
		if(toVector == Vector3.zero){
			return 0f;
		}
		float angle = Vector3.Angle(fromVector, toVector);
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}
	
}
