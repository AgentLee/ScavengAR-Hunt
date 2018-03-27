using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObject : MonoBehaviour 
{
	public bool collected;
	// Use this for initialization
	void Start () {
		collected = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(collected) {
			Debug.Log("Collected");
		}	
	}
}
