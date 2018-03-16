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
			// Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Collect()
	{
		if(gameObject.name == "Shield") {
			if(PlayerPrefs.HasKey("Shield")) {
				PlayerPrefs.SetInt(gameObject.name, PlayerPrefs.GetInt("Shield") + 1);		
			}
			else {
				PlayerPrefs.SetInt(gameObject.name, 1);
			}
		}
		else {
			PlayerPrefs.SetInt(gameObject.name, 1);
		}
		
		gameObject.SetActive(false);
		// Destroy(gameObject);
		// GameObject.Find("Manager").GetComponent<ScavengerManager>().DisplayItems();
	}
}
