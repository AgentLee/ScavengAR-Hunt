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
				++currPage;
				break;
			default:
				break;
		}
	}

	public void NextClue()
	{
		// Give Level 2 shield to player
		PlayerPrefs.SetInt("L2 Shield", 1);
		
		page1.SetActive(false);
		nextClue.SetActive(true);
	}
}
