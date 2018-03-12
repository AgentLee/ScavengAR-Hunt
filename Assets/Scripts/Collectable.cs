using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		if(PlayerPrefs.HasKey(gameObject.name)) {
			Debug.Log("Already collected item");
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Collect()
	{
		PlayerPrefs.SetInt(gameObject.name, 1);
		Destroy(gameObject);
		GameObject.Find("Manager").GetComponent<ScavengerManager>().DisplayItems();
	}
}
