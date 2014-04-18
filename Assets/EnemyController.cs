using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	public	Transform	playerModelGameObjectTrans;
	public	Transform[] changeMoveEnemyPoint;

	private NavMeshAgent agent;

	void Start(){
		agent = GetComponent<NavMeshAgent>();
	}

	void Update(){
		agent.SetDestination(changeMoveEnemyPoint[0].position);
		//EnemmyMove();
	}

	private void EnemmyMove(){
		Vector3 newRotation	= Quaternion.LookRotation(changeMoveEnemyPoint[0].position - playerModelGameObjectTrans.position).eulerAngles;
		newRotation.x		= 0;
		newRotation.z		= 0;
		playerModelGameObjectTrans.rotation = Quaternion.Slerp(playerModelGameObjectTrans.rotation, Quaternion.Euler(newRotation), Time.deltaTime);
	}
}
