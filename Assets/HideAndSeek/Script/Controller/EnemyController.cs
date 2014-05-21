using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour {

	public Transform[] wayPointIndex;
	public float fieldOfViewAngle = 110f;
	public float walkSpeed = 0.3f;
	public float dashSpeed = 4.75f;
	public float attackRange = 200f;
	public float stopRange = 100f;
	public float rimitChaseTimer = 5f;
	public GameObject bulletObj;

	private float chaseTimer = 0;
	private float endChaseTimer = 0;
	private float stopSpeed = 0;

	private NavMeshAgent navAgent;
	private Animator animtor;
	private EnemyAnimatorController animatorController;
	private SphereCollider opticSphereCol;
	private GameManager gameManager;
	private EnemyStatus enemyStatus;

	private GameObject playerGameObject;
	private	int patrolIndex;
	private Vector3 targetPosition;
	private bool isShooting;

	void Awake(){
		animtor = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		opticSphereCol = GetComponent<SphereCollider>();
		playerGameObject = GameObject.FindGameObjectWithTag(DoneTags.player);
		animatorController = GameObject.Find(PathHelper.gameManagerPath).GetComponent<EnemyAnimatorController>();
		gameManager = GameObject.Find(PathHelper.gameManagerPath).GetComponent<GameManager>();
		enemyStatus = gameObject.transform.FindChild(GameObjectNameHelper.robotGuardName).GetComponent<EnemyStatus>();

		animtor.SetLayerWeight(1, 1f);
		animtor.SetLayerWeight(2, 1f);
	}

	void Update(){
		if(gameManager.isSearchPlayer){
			Chase();
			//Shooting();
		}else{
			Patrol();
		}
	}


	void OnTriggerStay(Collider other){
		if(other.gameObject.name != GameObjectNameHelper.playerObjectName ){return;}
		Vector3 direction = other.transform.position - transform.position;
		float	angle = Vector3.Angle(direction, transform.forward);
		
		if(angle < fieldOfViewAngle*0.5f){
			RaycastHit hit;
			int layerMask = 1<<10;

			bool isFindPlayer = Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, opticSphereCol.radius, layerMask);
			if(!isFindPlayer && !gameManager.isSearchPlayer){return;}

			animtor.SetBool(animatorController.Chase, true);	//enemy shot animator
			gameManager.isSearchPlayer = true;
			navAgent.SetDestination(other.gameObject.transform.position);
		}
	}

	void OnAnimatorIK(int layerIndex){
		float aimWeight = animtor.GetFloat(animatorController.aimWeightFloat);
		animtor.SetIKPosition(AvatarIKGoal.RightHand, playerGameObject.transform.position + Vector3.up * 1.5f);
		animtor.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
	}

	//プレイヤーを追跡するメソッド.
	private void Chase(){
		float angle = FindAngle(transform.forward, playerGameObject.transform.position-this.transform.position, transform.up);
		Vector3 sightingDeltaPos = playerGameObject.transform.position - transform.position;

		if(sightingDeltaPos.sqrMagnitude > attackRange){
			animtor.SetFloat(animatorController.speedFloat, dashSpeed);
		}else{
			Shooting();
			isShooting = false;
			targetPosition = playerGameObject.transform.position - transform.position;
			navAgent.Stop();
			animtor.SetFloat(animatorController.speedFloat, stopSpeed);
		}

		if(sightingDeltaPos.sqrMagnitude > attackRange+10f){
			LostToPlayer();
		}
	}

	//エネミーの巡回メソッド.
	private void Patrol(){
		if(animtor.GetBool(animatorController.Chase)){
			animtor.SetBool(animatorController.Chase, false);
		}

		if(wayPointIndex.Length > 1){
			if(navAgent.remainingDistance >= navAgent.stoppingDistance){return;}
			navAgent.SetDestination(wayPointIndex[patrolIndex].position);
			float angle = FindAngle(transform.forward, wayPointIndex[patrolIndex].position-transform.position, transform.up);
			animtor.SetFloat(animatorController.angularSpeedFloat, angle);
			animtor.SetFloat(animatorController.speedFloat, walkSpeed);
			int targetLength = wayPointIndex.Length-1;
			patrolIndex = patrolIndex>=targetLength ? 0 : patrolIndex+1;
		}else{
			navAgent.Stop();
		}
	}

	private void Shooting(){
		chaseTimer += Time.deltaTime;
		if(chaseTimer >= 0.2f){
			Debug.Log(this.gameObject.name+"  shot!!");
			chaseTimer = 0;
			GameObject bulletClone = Instantiate(bulletObj, transform.position, transform.rotation) as GameObject;
			bulletClone.GetComponent<BulletController>().personEnemyObj = this.gameObject;
			bulletClone.GetComponent<BulletController>().attackPoint = enemyStatus.attack;
		}
	}

	private void LostToPlayer(){
		endChaseTimer += Time.deltaTime;
		if(endChaseTimer < rimitChaseTimer){return;}
		Debug.Log("lost player "+this.gameObject.name);
		gameManager.isSearchPlayer = false;
		patrolIndex = 0;
		navAgent.SetDestination(wayPointIndex[patrolIndex].position);
		float angle = FindAngle(transform.forward, wayPointIndex[patrolIndex].position-transform.position, transform.up);
		animtor.SetFloat(animatorController.angularSpeedFloat, angle);
		animtor.SetFloat(animatorController.speedFloat, walkSpeed);
		animtor.SetBool(animatorController.shoutingBool, false);
		animtor.SetBool(animatorController.Chase, false);
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
