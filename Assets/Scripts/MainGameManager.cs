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

	public GameObject play;
	public GameObject highScores;
	public GameObject continueAs;

	// Use this for initialization
	void Start () 
	{
		// Debug -- Run this to test loading screen
		// PlayerPrefs.DeleteAll();

		// Only need to run the player prefs at main menu.
		// if(SceneManager.GetActiveScene().buildIndex != (int)LEVELS.MAIN_MENU) {
		// 	return;
		// }

		// Get top score
		// GetComponent<HighScores>().RetrieveScores();

		// if(PlayerPrefs.GetInt("FirstRun") != 1) {
		// 	ShowMainMenuElements();
		// }
		// else {
		// 	PlayerPrefs.SetInt("FirstRun", 1);
		// 	ToggleContinue(true);
		// }

		if(PlayerPrefs.GetString("PlayerEmail") != "") {
			ToggleContinue(true);
		}
		else {
			ShowMainMenuElements();
		}
	}

	void ToggleContinue(bool status)
	{
		continueAs.SetActive(status);
		welcome.SetActive(status);

		if(status)
		{
			welcome.GetComponent<TMP_Text>().text = "Continue as " + PlayerPrefs.GetString("PlayerName") + "?";			
		}
	}

	public void ShowMainMenuElements()
	{
		ToggleContinue(false);

		play.SetActive(true);
		highScores.SetActive(true);

		print(PlayerPrefs.GetString("PlayerName") + " " + PlayerPrefs.GetString("PlayerEmail") + " " + PlayerPrefs.GetString("PlayerPhone") + " " + PlayerPrefs.GetInt("PlayerScore"));
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
		if(PlayerPrefs.GetString("PlayerEmail") == "") {
			LoadRegistration();
		}
		else {
			SceneManager.LoadScene((int)LEVELS.SPACE_INVADERS);
		}
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
