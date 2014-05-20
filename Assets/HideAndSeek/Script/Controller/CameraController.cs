using UnityEngine;
using System.Collections;

public enum CameraTest{
	type1,
	type2
}

public class CameraController : MonoBehaviour {
	public CameraTest type = CameraTest.type1;
	public Transform targetTrans;
	public float posY;

	private float smooth = 1.0f;
	private Vector3 vectorToCamera;
	private Vector3 xzVectorToCamera;
	private float distance = 2.5f;

	private void Awake(){
		switch(type){
		case CameraTest.type1:
			vectorToCamera = transform.position - targetTrans.position;
			xzVectorToCamera = new Vector3(vectorToCamera.x, 0, vectorToCamera.z);
			Debug.Log(string.Format("GAME START!! -----> vector to camera = {0}   xz vector to camera = {1}", vectorToCamera, xzVectorToCamera));
			break;
		case CameraTest.type2:
			vectorToCamera = transform.position - targetTrans.position;
			xzVectorToCamera = new Vector3(vectorToCamera.x, 0, vectorToCamera.z);
			break;
		}
	}

	private void Update(){
		Debug.Log("comera move now...");
		//Vector3 wandtedPos = targetTrans.position + xzVectorToCamera*distance;
		Vector3 wandtedPos = targetTrans.position;
		wandtedPos.y = targetTrans.position.y + posY;
		this.transform.position = Vector3.Lerp(this.transform.position, wandtedPos, smooth*Time.deltaTime);
		this.transform.LookAt(targetTrans);
	}
}