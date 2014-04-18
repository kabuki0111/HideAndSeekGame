using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	public	Transform	playerModelGameObjectTrans;
	public	Transform[] changeMoveEnemyPoint;
	public	float		fieldOfViewAngle		= 110f;				// Number of degrees, centred on forward, for the enemy see.
	//public	SphereCollider	col;

	private NavMeshAgent	agent;
	private bool			isPlayerInSight;
	private SphereCollider	col;
	private GameObject		playerGameObject;
	private	int				patrolIndex;
	private	float			stopChaseTimer;

	void Start(){
		agent				= GetComponent<NavMeshAgent>();
		col					= GetComponent<SphereCollider>();
		playerGameObject	= GameObject.FindGameObjectWithTag(DoneTags.player);
		patrolIndex			= 0;
	}

	void Update(){
		if(isPlayerInSight){
			agent.SetDestination(playerGameObject.transform.position);
			if(Vector3.Distance(transform.position, playerGameObject.transform.position) > 10f){
				stopChaseTimer += Time.deltaTime;
				if(stopChaseTimer > 5f){
					Debug.Log("stop!!");
					stopChaseTimer	= 0;
					isPlayerInSight = false;
				}
			}
		}else{
			Patrol();
		}
	}

	private void Patrol(){
		agent.SetDestination(changeMoveEnemyPoint[patrolIndex].position);
		float patrolPointX	= changeMoveEnemyPoint[patrolIndex].position.x;
		float patrolPointZ	= changeMoveEnemyPoint[patrolIndex].position.z;
		if(transform.position.x==patrolPointX && transform.position.z==patrolPointZ){
			patrolIndex ++;
			if(patrolIndex >= changeMoveEnemyPoint.Length){
				patrolIndex = 0;
			}
		}
	}

	private void  OutLook(Collider other){
		if(other.gameObject == playerGameObject){
			Vector3 direction	= other.transform.position - transform.position;
			float	angle		= Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f){
				RaycastHit	hit;
				var			layerMask		= 1<<10;
				bool		isFindPlayer	= Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, col.radius, layerMask);
				if(isFindPlayer){
					if(hit.collider.gameObject == playerGameObject){
						Debug.Log("hit player!!");
						isPlayerInSight = true;
					}
				}
			}
		}
	}

	void OnTriggerStay (Collider other){
		OutLook(other);
	}
	

}
