using UnityEngine;
using System.Collections;

public class gWalls : MonoBehaviour {

	public float CurrentTrackSpeed = 5f;
	public float IncrementalSpeed = 0.5f;
	public float InstanciateTime = 1f;
	public float DecrementalTime = 0.05f;

	public static gWalls s;
	void Awake()
	{
		s = this;
	}


}
