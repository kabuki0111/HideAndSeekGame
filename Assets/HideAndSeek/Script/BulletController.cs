using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float bulletSpeed = 2f;

	void Update(){
		this.transform.Translate(0, 0, bulletSpeed);
	}
}
