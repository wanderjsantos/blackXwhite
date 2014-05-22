using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public CharacterType type = CharacterType.None;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<Character>() == false ) return;

		if( other.gameObject.GetComponent<Character>().type != type ) Debug.Log("LOST");

	}
}
