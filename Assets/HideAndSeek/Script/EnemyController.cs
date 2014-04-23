using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	public Transform[] changeMoveEnemyPoint;
	public float fieldOfViewAngle = 110f;
	public float deadZone = 5f;

	private NavMeshAgent agent;
	private bool isPlayerInSight;
	private SphereCollider opticSphereCol;
	private GameObject playerGameObject;
	private	int patrolIndex;
	private	float stopChaseTimer;
	private Vector3 findPlayerLastPosition;

	private Animator animtor;
	private AnimatorStateInfo currentBaseState;
	private AnimatorSetup animSetup;

	//private DoneHashIDs hash;
	private HashIDs hash;

	void Awake(){
		animtor = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		opticSphereCol = GetComponent<SphereCollider>();

		playerGameObject = GameObject.FindGameObjectWithTag(DoneTags.player);

		hash = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<HashIDs>();
		animSetup = new AnimatorSetup(animtor, hash);

		patrolIndex = 0;
	}

	void Update(){
		if(isPlayerInSight){
			Chase();
		}else{
			Patrol();
		}

		NavAnimSetup();
	}
	 
	private void Chase(){
		float distanceEnemyAndPlayer = Vector3.Distance(transform.position, playerGameObject.transform.position);
		if(distanceEnemyAndPlayer > 10f){
			agent.SetDestination(findPlayerLastPosition);
			stopChaseTimer += Time.deltaTime;
			if(stopChaseTimer > 5f){
				Debug.Log("stop!!");
				stopChaseTimer = 0;
				isPlayerInSight = false;
			}
		}
	}
	
	private void Patrol(){
		agent.SetDestination(changeMoveEnemyPoint[patrolIndex].position);
		float patrolPointX = changeMoveEnemyPoint[patrolIndex].position.x;
		float patrolPointZ = changeMoveEnemyPoint[patrolIndex].position.z;
		//if(transform.position.x == patrolPointX && transform.position.z==patrolPointZ){
		//if(agent.destination.x == transform.position.x && agent.destination.z == transform.position.z){
		if(agent.destination == transform.position){
			patrolIndex ++;
			if(patrolIndex >= changeMoveEnemyPoint.Length){
				patrolIndex = 0;
			}
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
						//findPlayerLastPosition = hit.collider.gameObject.transform.position;
					}
				}
			}
		}
	}

	private void NavAnimSetup(){
		float speed;
		float angle;

		if(isPlayerInSight){
			speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
			angle = FindAngle(transform.forward, playerGameObject.transform.position - transform.position, transform.up);
		}else{
			speed = 1.5f;
			angle = FindAngle(transform.forward, agent.desiredVelocity, transform.up);
		}
		
		animSetup.Setup(speed, angle);
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
