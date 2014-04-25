using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform[] wayPointIndex;
	public float fieldOfViewAngle = 110f;

	private bool isPlayerInSight;
	private SphereCollider opticSphereCol;
	private GameObject playerGameObject;
	private	int patrolIndex = 0;
	private float speed = 0.1f;
	
	private NavMeshAgent navAgent;
	private Animator animtor;
	private EnemyAnimatorController animatorController;

	private float chaseTimer;
	private Vector3 hogeVec;	

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
		if(isPlayerInSight){
			Debug.Log("Chase !!");
			Chase();
		}else{
			Debug.Log("patrol!!");
			Patrol();
		}

	}

	void OnTriggerStay(Collider other){
		OutLook(other);
	}

	void OnAnimatorIK(int layerIndex)
	{
		float aimWeight = animtor.GetFloat(animatorController.aimWeightFloat);
		animtor.SetIKPosition(AvatarIKGoal.RightHand, playerGameObject.transform.position + Vector3.up * 1.5f);
		animtor.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}
	 
	private bool isFlag = true;
	private void Chase(){
		Vector3 sightingDeltaPos = playerGameObject.transform.position - transform.position;
		if(sightingDeltaPos.sqrMagnitude < 50f){
			Debug.Log("type == 1");
			navAgent.Stop();
		}else if(sightingDeltaPos.sqrMagnitude >=50f && sightingDeltaPos.sqrMagnitude <200f){
			Debug.Log("type == 2");
			animtor.SetBool(animatorController.shoutingBool, true);
			navAgent.Stop();
			AnimatorControl(0, 0);
		}else{
			Debug.Log("type == 3");
			if(isFlag){
				isFlag = false;
				chaseTimer = 0;
				hogeVec = playerGameObject.transform.position - transform.position;
				navAgent.SetDestination(hogeVec);
				Debug.Log("input pos!! >>>>> "+hogeVec);
			}
		}

		if(!isFlag){
			chaseTimer += Time.deltaTime;
			if(chaseTimer>=2f){
				isFlag = true;
			}
		}

	}
	
	private void Patrol(){
		if(navAgent.remainingDistance < navAgent.stoppingDistance){
			navAgent.SetDestination(wayPointIndex[patrolIndex].position);
			float angle = FindAngle(transform.forward, wayPointIndex[patrolIndex].position-transform.position, transform.up);
			AnimatorControl(speed, angle);
			int targetLength = wayPointIndex.Length-1;
			patrolIndex = patrolIndex>=targetLength ? 0 : patrolIndex+1;
		}
	}

	private void  OutLook(Collider other){
		if(other.gameObject == playerGameObject){
			Vector3 direction = other.transform.position - transform.position;
			float	angle = Vector3.Angle(direction, transform.forward);
			if(angle >= fieldOfViewAngle*0.5){return;}
			RaycastHit hit;
			int layerMask = 1<<10;
			bool isFindPlayer = Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, opticSphereCol.radius, layerMask);
			if(!isFindPlayer){return;}
			AnimatorControl(4.75f, 0);
			animtor.SetBool(animatorController.Chase, true);
			isPlayerInSight = true;
		}
	}

	private void AnimatorControl(float speed, float angle){
		animtor.SetFloat(animatorController.speedFloat, speed);
		animtor.SetFloat(animatorController.angularSpeedFloat, angle);
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
