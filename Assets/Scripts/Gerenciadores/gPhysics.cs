using UnityEngine;
using System.Collections;

public class gPhysics : MonoBehaviour {

	public bool 	PhysicEnable = true;
	public float 	ConstanteGravitacional = 6.6f;
	public float 	WalkingForce = 10f;
	public float 	JumpImpulseForce = 7f;


	public static gPhysics s;
	void Awake() 
	{
		s = this;
	}
}
