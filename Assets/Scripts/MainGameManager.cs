/*
 * This script is used to maintain the state of the game.
 * It should have references to Space InvadARs and the ScavengAR Hunt.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum LEVELS
{
	SCAVENGER_HUNT 	= 0,
	SPACE_INVADERS 	= 1,
	MAIN_MENU		= 4,
	HIGH_SCORES 	= 5,
}

public class MainGameManager : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void LoadScavengerHunt()
	{
		SceneManager.LoadScene((int)LEVELS.SCAVENGER_HUNT);
	}

	public void LoadSpaceInvaders()
	{
		SceneManager.LoadScene((int)LEVELS.SPACE_INVADERS);
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene((int)LEVELS.MAIN_MENU);
	}

	public void LoadHighScores()
	{
		SceneManager.LoadScene((int)LEVELS.HIGH_SCORES);
	}
}
