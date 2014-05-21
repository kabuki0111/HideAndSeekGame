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

	private GameObject targetObject;
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
			targetObject = GameObject.FindWithTag(GameObjectNameHelper.playerObjectName);
			break;
		}
	}


	private void Update(){
		switch(type){
		case CameraTest.type1:
			Debug.Log("comera move now...");
			Vector3 wandtedPos = targetTrans.position;
			wandtedPos.y = targetTrans.position.y + posY;
			this.transform.position = Vector3.Lerp(this.transform.position, wandtedPos, smooth*Time.deltaTime);
			this.transform.LookAt(targetTrans);
			break;
		case CameraTest.type2:
			Vector3 targetPos = (targetObject.transform.position) - targetObject.transform.forward * 7.5f;
			targetPos.y = targetTrans.position.y + posY;
			transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime*1f);
			Vector3 toward = ( targetObject.transform.position + targetObject.transform.rotation * Vector3.forward * 5.0f ) - transform.position;
			Quaternion targetRot = Quaternion.LookRotation( toward, Vector3.up );
			Quaternion currentRot = Quaternion.Slerp( transform.rotation, targetRot, Time.deltaTime * 1f );
			transform.rotation = currentRot;
			break;
		}
	}


}