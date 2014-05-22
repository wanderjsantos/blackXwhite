using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//[RequireComponent(typeof(Rigidbody))]
public delegate void 	RightMoveEventHandler(object sender, EventArgs e);
public delegate void 	LeftMoveEventHandler(object sender, EventArgs e);
public delegate void 	UpMoveEventHandler(object sender, EventArgs e);
public delegate void 	ChangeMoveEventHandler(object sender, EventArgs e);

public class Controls : MonoBehaviour 
{
	public event RightMoveEventHandler 		RightMove;
	public event LeftMoveEventHandler 		LeftMove;
	public event UpMoveEventHandler 		UpMove;
	public event ChangeMoveEventHandler 	ChangeMove;

	void Update()
	{
		if(Input.GetKey(KeyCode.RightArrow))
		{
			if( RightMove != null ) RightMove( this, EventArgs.Empty );
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			if( LeftMove != null ) LeftMove( this, EventArgs.Empty );
		}

		if(gControls.s.DoCommand("KeyUp") || gControls.s.DoMouseCommand("Mouse0Down") )
		{
			if(UpMove != null ) UpMove(this, EventArgs.Empty );
		}

		if( gControls.s.DoCommand("KeyDown") || gControls.s.DoMouseCommand("Mouse0Drag"))
		{
			if(ChangeMove != null ) ChangeMove(this, EventArgs.Empty);
		}
	}

}
