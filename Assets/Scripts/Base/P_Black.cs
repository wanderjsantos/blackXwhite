using UnityEngine;
using System.Collections;

public class P_Black : MonoBehaviour {

	Vector3 refVel;
	public void ChangePosition(GameObject go)
	{
		Vector3 oldPosition = go.transform.position;
		Vector3 newPosition = new Vector3(oldPosition.x, -1 * oldPosition.y, oldPosition.z ) ;
		
		StartCoroutine(_WaitToActivateCollider(go, newPosition));
	}
	
	IEnumerator _WaitToActivateCollider(GameObject go, Vector3 newPos)
	{
		while( Mathf.Abs( go.transform.position.magnitude - newPos.magnitude ) < 0.01f )
		{
			go.transform.position = Vector3.SmoothDamp( go.transform.position, newPos,ref refVel, 0.5f );
			yield return null;
		}
		
		go.transform.position = newPos;
		yield return null;
	}
}
