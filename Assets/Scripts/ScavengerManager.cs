using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScavengerManager : MonoBehaviour 
{
	public bool rothPowerup;

	// Use this for initialization
	void Start () 
	{
		rothPowerup = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(rothPowerup) {
			Debug.Log("COLLECTED ITEM");
		}
	}

	public void openRothLink()
	{
		Application.OpenURL("http://www.cis.upenn.edu/~aaroth/");
	}

	public void playSpaceInvadARs()
	{
		Application.LoadLevel(1);
	}
}
