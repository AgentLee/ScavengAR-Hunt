/*
 * This script is used to maintain the state of the game.
 * It should have references to Space InvadARs and the ScavengAR Hunt.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

enum LEVELS
{
	MAIN_MENU		= 0,
	SCAVENGER_HUNT 	= 1,
	SPACE_INVADERS 	= 2,
	HIGH_SCORES 	= 5,
	REGISTRATION	= 6,
}

public class MainGameManager : MonoBehaviour 
{
	public GameObject welcome;

	// Use this for initialization
	void Start () 
	{
		// Debug
		// PlayerPrefs.DeleteAll();

		// First time, have player register with email
		if(PlayerPrefs.GetString("PlayerEmail") == "") {
			welcome.SetActive(false);
			LoadRegistration();
		}
		else {
			welcome.SetActive(true);
			welcome.GetComponent<TMP_Text>().text = "Welcome back " + PlayerPrefs.GetString("PlayerName") + "!";
		}
	}
	
	public void LoadRegistration()
	{
		SceneManager.LoadScene((int)LEVELS.REGISTRATION);
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

public struct PlayerScore 
{
	public string username;
	public int score;
	public int phone;
	public string email;

	public PlayerScore(string _username, int _score, int _phone, string _email)
	{
		username = _username;
		score = _score;
		phone = _phone;
		email = _email;
	}
}
