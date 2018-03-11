using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Collect()
	{
		PlayerPrefs.SetInt(gameObject.name, 1);
		Destroy(gameObject);
		// GameObject.Find("Manager").GetComponent<ScavengerManager>().DisplayItems();
	}
}
