using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;
	//private const ENMMY_POINT_GAMEOBJECT_PATH = "EnemyAllRootObject";
	public	Transform	playerModelGameObjectTrans;
	public Transform[] changeMoveEnemyPoint;


	void Update(){
		Vector3 newRotation	= Quaternion.LookRotation(changeMoveEnemyPoint[0].position - playerModelGameObjectTrans.position).eulerAngles;
		newRotation.x		= 0;
		newRotation.z		= 0;
		playerModelGameObjectTrans.rotation = Quaternion.Slerp(playerModelGameObjectTrans.rotation, Quaternion.Euler(newRotation), Time.deltaTime);

	}

	private void EnemmyMove(Transform enemmyTrans){
//		float	axisHorizontalValue	= enemmyTrans.position.x * MOVE_SPEED_ADJUSTMENT;
//		float	axisVerticalValue	= enemmyTrans.position.y * MOVE_SPEED_ADJUSTMENT;
//		transform.rotation = Quaternion.LookRotation(enemmyTrans.position);
//		transform.position += new Vector3(axisHorizontalValue, 0, axisVerticalValue);
//		var newRotation = Quaternion.LookRotation(enemmyTrans.position - transform.position).eulerAngles;
//		var angles		= transform.rotation.eulerAngles;
//		transform.rotation = Quaternion.Euler(angles.x, 0, angles.z);
	}
}
