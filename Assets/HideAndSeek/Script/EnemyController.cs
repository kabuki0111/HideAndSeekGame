using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	public Transform[] changeMoveEnemyPoint;
	public float fieldOfViewAngle = 110f;

	private NavMeshAgent navAgent;
	private bool isPlayerInSight;
	private SphereCollider opticSphereCol;
	private GameObject playerGameObject;
	private	int patrolIndex = 0;
	private	float stopChaseTimer;
	private Vector3 findPlayerLastPosition;
	private float speed = 0.1f;

	private Animator animtor;
	private AnimatorStateInfo currentBaseState;
	private AnimatorSetup animSetup;

	//private DoneHashIDs hash;
	private HashIDs hash;

	void Awake(){
		animtor = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		opticSphereCol = GetComponent<SphereCollider>();
		playerGameObject = GameObject.FindGameObjectWithTag(DoneTags.player);

	}

	void Update(){
		if(isPlayerInSight){
			Debug.Log("chase!!");
			//Chase();
		}else{
			Debug.Log("patrol!!");
			Patrol();
		}

	}
	 
	private void Chase(){
		float distanceEnemyAndPlayer = Vector3.Distance(transform.position, playerGameObject.transform.position);
		if(distanceEnemyAndPlayer > 10f){
			navAgent.SetDestination(findPlayerLastPosition);
			stopChaseTimer += Time.deltaTime;
			if(stopChaseTimer > 5f){
				Debug.Log("stop!!");
				stopChaseTimer = 0;
				isPlayerInSight = false;
			}
		}
	}
	
	private void Patrol(){
		if(navAgent.remainingDistance < navAgent.stoppingDistance){
			navAgent.SetDestination(changeMoveEnemyPoint[patrolIndex].position);
			float angle = FindAngle(transform.forward, changeMoveEnemyPoint[patrolIndex].position-transform.position, transform.up);
			Debug.Log("angle --->"+angle);
			AnimatorControl(speed, angle);
			int targetLength = changeMoveEnemyPoint.Length-1;
			patrolIndex = patrolIndex>=targetLength ? 0 : patrolIndex+1;
		}
	}

	private void  OutLook(Collider other){
		if(other.gameObject == playerGameObject){
			Vector3 direction = other.transform.position - transform.position;
			float	angle = Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f){
				RaycastHit hit;
				var layerMask = 1<<10;
				bool isFindPlayer = Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, opticSphereCol.radius, layerMask);
				if(isFindPlayer){
					if(hit.collider.gameObject == playerGameObject){
						Debug.Log("find player game object!!");
						isPlayerInSight = true;
					}
				}
			}
		}
	}

	private void AnimatorControl(float speed, float angle){
		animtor.SetFloat("Speed", speed);
		animtor.SetFloat("Direction", angle);
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

	void OnTriggerStay (Collider other){
		OutLook(other);
	}
	

}
