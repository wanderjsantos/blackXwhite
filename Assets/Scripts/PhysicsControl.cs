using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsControl : MonoBehaviour 
{
	public List<GravityBehavior> allAttractiveItens;

	Vector3 distanciaEntreCorpos;	

	void Awake()
	{
		foreach(GravityBehavior go in GameObject.FindObjectsOfType(typeof(GravityBehavior)) as GravityBehavior[])
		{
			allAttractiveItens.Add(go);
		}
	}
	
    public void Update () 
	{
		if(gPhysics.s.PhysicEnable == false) return;
		for( int i =0; i < allAttractiveItens.Count ; i++)
		{
			for( int j =0; j < allAttractiveItens.Count ; j++)
			{
				GravityBehavior currentBehavior = allAttractiveItens[i];
				GravityBehavior nextBehavior = allAttractiveItens[j];
				
				if( i == j ) continue;
																	
		        distanciaEntreCorpos = nextBehavior.transform.position - currentBehavior.transform.position;
		        distanciaEntreCorpos.z = 0;		
		        
				if( distanciaEntreCorpos.magnitude <= ((nextBehavior.collider.bounds.size.x + currentBehavior.collider.bounds.size.x)/2)+0.01f )continue;
				
				if (distanciaEntreCorpos.magnitude > 0.0001f)
		        {
					//Lei da gravitaçao universal F = G(m1*m2)/Dis²;
					float f = (gPhysics.s.ConstanteGravitacional * (( currentBehavior.rigidbody.mass * nextBehavior.rigidbody.mass)/ Mathf.Sqrt(distanciaEntreCorpos.magnitude) ))*Time.deltaTime;
					currentBehavior.rigidbody.AddForce( f * distanciaEntreCorpos.normalized );
		        }
			}
		}
	    
	}

	public void ChangePosition()
	{


	}
	
}
