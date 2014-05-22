using UnityEngine;
using System.Collections;

public enum CharacterType{Black, White, None}

public class Character : GravityBehavior, ICharacter {

	public CharacterType type = CharacterType.None;
	Vector3 refVel;
	public  Vector3 mUp;
	public bool jumping;
	public bool changing = false;

	void Start()
	{
		if(transform.position.y > 0) mUp = Vector3.up;
		else mUp = -Vector3.up;
	}

	//Inverte a posicao y dos personagens
	public void ChangePosition(GameObject go)
	{
		Vector3 oldPosition = go.transform.position;
		Vector3 newPosition = new Vector3(oldPosition.x, -1 * oldPosition.y, oldPosition.z ) ;

		StartCoroutine(_WaitToActivateCollider(go, newPosition));
	}

	//Caminhar
	public void Move(Vector3 ax )
	{
		rigidbody.AddForce(ax.normalized * gPhysics.s.WalkingForce);
	}

	//Pular
	public void Jump()
	{
		if(jumping) return;

		//rigidbody.AddForce(mUp.normalized * gPhysics.s.JumpImpulseForce, ForceMode.Impulse);
		Vector3 vel = rigidbody.velocity;
		vel.y = (mUp.normalized * gPhysics.s.JumpImpulseForce).y;
		rigidbody.velocity = vel;
		jumping = true;
	}

	//Desativa o Collider para evitar colisoes enquanto troca de posicao
	//reativa-o quando chegar ao destino
	IEnumerator _WaitToActivateCollider(GameObject go, Vector3 newPos)
	{
		if( changing ) yield break;

		changing = true;
		gPhysics.s.PhysicEnable = false;

		go.collider.enabled = false;

		Vector3 tempPos = go.transform.position;
		Vector3 tempVelocity = go.rigidbody.velocity;

		go.rigidbody.isKinematic = true;
		tempVelocity.y *= -1;

		while( Mathf.Abs( (go.transform.position - newPos).magnitude ) > 0.01f )
		{
			tempPos = Vector3.SmoothDamp( tempPos, newPos,ref refVel, .07f );
			go.rigidbody.MovePosition(tempPos);
			yield return null;
		}

		go.rigidbody.isKinematic = false;
		go.rigidbody.velocity = tempVelocity;

		mUp *= -1;

 		go.collider.enabled = true;
		gPhysics.s.PhysicEnable = true;
		changing = false;

		yield return null;
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.layer != LayerMask.NameToLayer("Level")) return;

		jumping = false;
	}
}
