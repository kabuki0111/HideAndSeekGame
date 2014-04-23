﻿using UnityEngine;
using System.Collections;

public class TestNav : MonoBehaviour {
	public Transform[] target;

	private NavMeshAgent navAgent;
	private Animator animator;
	private float speed = 0.1f;
	private int pointNum = 0;

	void Awake(){
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	void Update(){

		bool isFlag = FindGoalPoint(target[pointNum]);

		if(isFlag){
			animator.SetFloat("Speed", 0);
			animator.SetFloat("Direction", 0);

			if(pointNum>=(target.Length-1)){
				pointNum--;
			}else{
				pointNum++;
			}
			Debug.Log("point Number --->"+pointNum);
		}else{
			navAgent.SetDestination(target[pointNum].position);
			float angle = FindAngle(transform.forward, target[pointNum].position-transform.position, transform.up);
			animator.SetFloat("Speed", speed);
			animator.SetFloat("Direction", angle);
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

	private bool FindGoalPoint(Transform targetTrans){
		bool isGoalPoint = false;
		float x1 = targetTrans.position.x - 5f;
		float x2 = targetTrans.position.x + 5f;
		float z1 = targetTrans.position.z - 5f;
		float z2 = targetTrans.position.z + 5f;

		if(transform.position.x>x1 && transform.position.x<x2){
			if(transform.position.z>z1 && transform.position.z<z2){
				Debug.Log("chaeck point!!");
				isGoalPoint = true;
			}
		}
		return isGoalPoint;
	}

	 
}
