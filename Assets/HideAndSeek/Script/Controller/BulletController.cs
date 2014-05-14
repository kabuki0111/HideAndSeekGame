using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float bulletSpeed = 2f;
	private GameObject __personEnemyObj;
	private int __attackPoint;

	public GameObject personEnemyObj{get; set;}
	public int attackPoint{get; set;}

	void Update(){
		this.transform.Translate(0, 0, bulletSpeed);
		if(Vector3.Distance(personEnemyObj.transform.position, this.gameObject.transform.position)>100f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == GameObjectTagHelper.EnemyTagName) {return;}
		if(other.gameObject.tag == GameObjectTagHelper.DamageRegionTagName){return;}

		if(other.gameObject.tag == GameObjectTagHelper.PlayerTagName){
			other.gameObject.GetComponent<PlayerStatus>().DamagePlayerHitPoint(attackPoint);
		}else{
			Destroy(this.gameObject);
		}
	}
	
}
