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

	void Start(){
		agent				= GetComponent<NavMeshAgent>();
		col					= GetComponent<SphereCollider>();
		playerGameObject	= GameObject.FindGameObjectWithTag(DoneTags.player);
	}

	void Update(){
		if(isPlayerInSight){
			agent.SetDestination(playerGameObject.transform.position);
		}else{
		 	agent.SetDestination(changeMoveEnemyPoint[0].position);
		}
	}


	void OnTriggerStay (Collider other){
		if(other.gameObject == playerGameObject){
			Debug.Log("UC");
			isPlayerInSight = false;

			Vector3 direction	= other.transform.position - transform.position;
			float	angle		= Vector3.Angle(direction, transform.forward);
			
			if(angle < fieldOfViewAngle * 0.5f){
				RaycastHit hit;
				var layerMask =  1<<8;
				if(Physics.Raycast(transform.position+transform.up, direction.normalized, out hit, col.radius, layerMask)){
					Debug.Log("agagagas ----> "+hit.collider.gameObject.name);
					if(hit.collider.gameObject == playerGameObject){
						isPlayerInSight = true;
						Debug.Log(">>>> "+isPlayerInSight);
					}
				}
			}

		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
//		if(other.gameObject == playerGameObject){
//			isPlayerInSight = false;
//		}
	}
}
