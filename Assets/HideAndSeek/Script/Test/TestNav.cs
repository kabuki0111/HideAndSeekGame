using UnityEngine;
using System.Collections;

public class TestNav : MonoBehaviour {
	public Transform[] target;

	private NavMeshAgent navAgent;
	private Animator animator;
	private float speed = 0.1f;
	private int pointNum = 0;
	private float timer = 0;

	void Awake(){
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	void Update(){
		if(navAgent.remainingDistance < navAgent.stoppingDistance){
			navAgent.SetDestination(target[pointNum].position);
			float angle = FindAngle(transform.forward, target[pointNum].position-transform.position, transform.up);
			AnimatorControl(speed, angle);
			int targetLength = target.Length-1;
			pointNum = pointNum>=targetLength ? pointNum-1 : pointNum+1; 
		}
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
	
	private void AnimatorControl(float speed, float angle){
		animator.SetFloat("Speed", speed);
		animator.SetFloat("Direction", angle);
	}
}
