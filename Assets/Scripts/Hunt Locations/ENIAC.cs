/*
 * This script handles events when the players's image target is at the ENIAC.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENIAC : MonoBehaviour 
{
	GameObject intro;
	GameObject page1;
	GameObject nextClue;

	int currPage;
	
	// Use this for initialization
	void Start () 
	{
		intro 		= transform.Find("ENIAC").gameObject;
		page1 		= transform.Find("AI Intro").gameObject;
		nextClue 	= transform.Find("Next Clue").gameObject;
		currPage 	= 0;
	}
	
	public void Continue()
	{
		switch(currPage) 
		{
			case 0:
				intro.SetActive(false);
				page1.SetActive(true);
				++currPage;
				break;
			case 1:
				page1.SetActive(false);
				nextClue.SetActive(true);
				// Give Level 2 shield to player
				PlayerPrefs.SetInt("PowerUpShield", 1);
				Debug.Log("Shield: " + PlayerPrefs.GetInt("PowerUpShield"));
				++currPage;
				break;
			default:
				break;
		}
	}

	public void NextClue()
	{
		// Give Level 2 shield to player
		PlayerPrefs.SetInt("PowerUpShield", 1);
		Debug.Log("Shield: " + PlayerPrefs.GetInt("PowerUpShield"));

		// PlayerPrefs.SetInt("PowerUpShield", 0);		// ENIAC 	- Shield		- L2
		// PlayerPrefs.SetInt("PowerUpBar", 0);		// AddLab 	- Bar			- L3 (if completed)
		// PlayerPrefs.SetInt("PowerUpFish", 0);		// GRASP 	- Franklin Fish	- Life
		// PlayerPrefs.SetInt("PowerUpBook", 0);		// SigLab 	- Book			- 
		// PlayerPrefs.SetInt("PowerUpPenn", 0);		// SigLab 	- Penn Dots		- 

		page1.SetActive(false);
		nextClue.SetActive(true);
	}
}
