using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class GravityBehavior : MonoBehaviour , iGravityBehavior
{
	public void Test()
	{}
	
	
	
}

public interface iGravityBehavior
{
	void Test();
}
