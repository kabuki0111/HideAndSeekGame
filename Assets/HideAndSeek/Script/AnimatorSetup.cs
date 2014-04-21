using UnityEngine;
using System.Collections;

public class AnimatorSetup{
	public float speedDampTime = 0.1f;
	public float angularSpeedDampTime = 0.7f;
	public float angleResponseTime = 0.6f;

	private Animator anim;
	private DoneHashIDs hash;
	
	
	// Constructor
	public AnimatorSetup(Animator animator, DoneHashIDs hashIDs)
	{
		anim = animator;
		hash = hashIDs;
	}
	
	
	public void Setup(float speed, float angle)
	{
		float angularSpeed = angle / angleResponseTime;
		
		// Set the mecanim parameters and apply the appropriate damping to them.
		anim.SetFloat(hash.speedFloat, speed, speedDampTime, Time.deltaTime);
		anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}	

}
