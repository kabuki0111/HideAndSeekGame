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
			Debug.Log("Chase !!");
			Chase();
		}else{
			Debug.Log("patrol!!");
			Patrol();
		}

	}

	void OnTriggerStay (Collider other){
		OutLook(other);
	}
	 
	private void Chase(){
		Vector3 sightingDeltaPos = playerGameObject.transform.position - transform.position;

		if(sightingDeltaPos.sqrMagnitude > 50f){
			float angle = FindAngle(transform.forward, playerGameObject.transform.position-transform.position, transform.up);
			AnimatorControl(0.7f, angle);
			navAgent.destination = playerGameObject.transform.position;
		}else{
			navAgent.Stop();
			Debug.Log("Stop!!");
			AnimatorControl(0, 0);
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

			if(!isFindPlayer){	
				Debug.Log("find player game object!!  "+isFindPlayer);
				isPlayerInSight = true;
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


	

}
