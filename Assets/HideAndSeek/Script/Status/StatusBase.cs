using UnityEngine;
using System.Collections;

public class StatusBase : MonoBehaviour {
	private int __hp;
	private int __attack;

	public int hp{get; set;}
	public int attack{get; set;}

	protected virtual void Awake(){
	}

	protected virtual void Update(){
	}
}
