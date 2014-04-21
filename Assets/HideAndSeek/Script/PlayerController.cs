using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private const float MOVE_SPEED_ADJUSTMENT = 0.05f;

	void Update () {
		float axisHorizontalValue = Input.GetAxis("Horizontal")	* MOVE_SPEED_ADJUSTMENT;
		float axisVerticalValue = Input.GetAxis("Vertical")		* MOVE_SPEED_ADJUSTMENT;
		Vector3 axisTotalVector3 = new Vector3(axisHorizontalValue, 0, axisVerticalValue);

		transform.rotation = Quaternion.LookRotation(axisTotalVector3);
		transform.position += axisTotalVector3;
	}
}
