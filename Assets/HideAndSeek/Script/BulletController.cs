using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float bulletSpeed = 2f;
	private GameObject __personEnemyObj;

	public GameObject personEnemyObj{get; set;}

	void Update(){
		this.transform.Translate(0, 0, bulletSpeed);
		if(Vector3.Distance(personEnemyObj.transform.position, this.gameObject.transform.position)>100f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag != GameObjectNameHelper.EnemyTagName){
			Debug.Log(other.gameObject.name);
			Destroy(this.gameObject);
		}
	}
}
