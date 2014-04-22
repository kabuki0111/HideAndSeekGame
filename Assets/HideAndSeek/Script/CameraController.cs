using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform targetTrans;
	//private Transform targetTrans;

	private float smooth = 1.0f;
	private Vector3 vectorToCamera;
	private Vector3 xzVectorToCamera;
	private float distance = 1.5f;

	void Awake(){
		vectorToCamera = transform.position - targetTrans.position;
		xzVectorToCamera = new Vector3(vectorToCamera.x, 0, vectorToCamera.z);
	}

	void Update(){
		Vector3 wandtedPos = targetTrans.position + xzVectorToCamera*distance;
		wandtedPos.y = targetTrans.position.y + 4.5f;
		transform.position = Vector3.Lerp(transform.position, wandtedPos, smooth*Time.deltaTime);
	}
}