using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Track : MonoBehaviour {

	public Vector3 direction = Vector3.left;
	public Vector3 limit = new Vector3( -20, 0, 0);
	public float timeToInit = 2f;
	public float currentTime = 2f;


	public List<GameObject> obstaclesQueue;
	List<GameObject> obstaclesInScene = new List<GameObject>();

	void Start()
	{
		Init();
	}


	void Update()
	{
		for(int i = 0 ; i < transform.childCount; i ++ )
		{
			if(transform.GetChild(i) == transform) continue;

			transform.GetChild(i).Translate(direction * gWalls.s.CurrentTrackSpeed * Time.deltaTime);

			if( transform.GetChild(i).position.x < limit.x ) DestroyObstacle(transform.GetChild(i).gameObject);
		}

		if( Time.realtimeSinceStartup > (currentTime+gWalls.s.InstanciateTime) ) InstantiateObstacle();
	}

	void Init()
	{
		Invoke( "InstantiateObstacle", timeToInit );

	}

	public void InstantiateObstacle()
	{
		if( obstaclesQueue.Count == 0 )
		{
			Debug.Log("End of Queue");
			return;
		}

		GameObject go = Instantiate( obstaclesQueue[0] )as GameObject;

		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;

		obstaclesQueue.RemoveAt(0);
		obstaclesInScene.Add(go);

		currentTime = Time.realtimeSinceStartup;

	}	



	void DestroyObstacle (GameObject go)
	{
		if( obstaclesInScene.Contains(go)) obstaclesInScene.Remove(go);
		Destroy(go);
	}
}
