using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	public Controls controls;
	public Character[]characteres;

	void Awake()
	{
		characteres = gameObject.GetComponentsInChildren<Character>(true) as Character[];

		controls = gameObject.GetComponent<Controls>();
		if(controls == null ) return;

		//controls.RightMove += HandleRightMove;
		//controls.LeftMove += HandleLeftMove;
		controls.UpMove += HandleUpMove;
		controls.ChangeMove += HandleChangeMove;


	}


	void HandleChangeMove (object sender, System.EventArgs e)
	{
		ChangeCharacteres();
	}

	void HandleUpMove (object sender, System.EventArgs e)
	{
		Jump();
	}

	void HandleLeftMove (object sender, System.EventArgs e)
	{
		MoveCharacteres(-Vector3.right);
	}

	void HandleRightMove (object sender, System.EventArgs e)
	{
		MoveCharacteres(Vector3.right);
	}

	public void MoveCharacteres( Vector3 direction )
	{
		characteres[0].Move(direction);
		characteres[1].Move(direction);
	}

	public void ChangeCharacteres()
	{
		characteres[0].ChangePosition(characteres[0].gameObject);
		characteres[1].ChangePosition(characteres[1].gameObject);
	}

	public void Jump()
	{
		characteres[0].Jump();
		characteres[1].Jump();
	}

}

public interface ICharacter
{
	void ChangePosition( GameObject gameObject );
	void Move(Vector3 axis);
}
