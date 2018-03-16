using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RothItem : MonoBehaviour 
{
	private bool display;

	public ScavengerManager scavengerManager;

	// Use this for initialization
	void Start () 
	{
		display = false;
		StartCoroutine(DisplayItem());
	}
	
	IEnumerator DisplayItem()
	{
		yield return new WaitForSeconds(15);
		display = true;
	}

	// Update is called once per frame
	void Update () 
	{
		if(display) {
			GetComponent<MeshRenderer>().enabled = true;

			transform.Rotate(Vector3.up, 5.0f * Time.deltaTime);
		}
		else {
			GetComponent<MeshRenderer>().enabled = false;			
		}
	}

	public void CollectItem()
	{
		// scavengerManager.rothPowerup = true;
		Destroy(gameObject);
	}
}
