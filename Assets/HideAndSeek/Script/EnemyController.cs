using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	public float animSpeed = 1.5f;				// アニメーション再生速度設定
	public Transform playerModelGameObjectTrans;
	public Transform[] changeMoveEnemyPoint;
	public float fieldOfViewAngle = 110f;				// Number of degrees, centred on forward, for the enemy see.
	public float deadZone = 5f;

	private NavMeshAgent agent;
	private bool isPlayerInSight;
	private SphereCollider opticSphereCol;
	private GameObject playerGameObject;
	private	int patrolIndex;
	private	float stopChaseTimer;

	private Animator anim;								// キャラにアタッチされるアニメーターへの参照
	private AnimatorStateInfo currentBaseState;			// base layerで使われる、アニメーターの現在の状態の参照
	private DoneAnimatorSetup animSetup;				// An instance of the AnimatorSetup helper class.

	private DoneHashIDs hash;

	void Awake(){
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		opticSphereCol = GetComponent<SphereCollider>();
		playerGameObject = GameObject.FindGameObjectWithTag(DoneTags.player);
		hash = GameObject.FindGameObjectWithTag(DoneTags.gameController).GetComponent<DoneHashIDs>();
		animSetup = new DoneAnimatorSetup(anim, hash);
		patrolIndex = 0;
	}

	void Update(){
		if(isPlayerInSight){
			agent.SetDestination(playerGameObject.transform.position);
			if(Vector3.Distance(transform.position, playerGameObject.transform.position) > 10f){
				stopChaseTimer += Time.deltaTime;
				if(stopChaseTimer > 5f){
					Debug.Log("stop!!");
					stopChaseTimer = 0;
					isPlayerInSight = false;
				}
			}
		}else{
			Patrol();
		}

		NavAnimSetup();
//		animSetup.Setup(speed, angle);
	}

	private void Patrol(){
		agent.SetDestination(changeMoveEnemyPoint[patrolIndex].position);
		float patrolPointX = changeMoveEnemyPoint[patrolIndex].position.x;
		float patrolPointZ = changeMoveEnemyPoint[patrolIndex].position.z;
		if(transform.position.x == patrolPointX && transform.position.z==patrolPointZ){
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
						Debug.Log("hit player!!");
						isPlayerInSight = true;
					}
				}
			}
		}
	}

	private void NavAnimSetup()
	{
		// Create the parameters to pass to the helper function.
		float speed;
		float angle;
		
		// If the player is in sight...
		if(isPlayerInSight)
		{
			// ... the enemy should stop...
			speed = 0f;
			
			// ... and the angle to turn through is towards the player.
			angle = FindAngle(transform.forward, playerGameObject.transform.position - transform.position, transform.up);
		}
		else
		{
			// Otherwise the speed is a projection of desired velocity on to the forward vector...
			speed = Vector3.Project(agent.desiredVelocity, transform.forward).magnitude;
			
			// ... and the angle is the angle between forward and the desired velocity.
			angle = FindAngle(transform.forward, agent.desiredVelocity, transform.up);
			
			// If the angle is within the deadZone...
			if(Mathf.Abs(angle) < deadZone)
			{
				// ... set the direction to be along the desired direction and set the angle to be zero.
				transform.LookAt(transform.position + agent.desiredVelocity);
				angle = 0f;
			}
		}
		
		// Call the Setup function of the helper class with the given parameters.
		animSetup.Setup(speed, angle);
	}
	
	private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
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
