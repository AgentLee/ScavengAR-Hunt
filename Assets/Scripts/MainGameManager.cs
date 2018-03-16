﻿/*
 * This script is used to maintain the state of the game.
 * It should have references to Space InvadARs and the ScavengAR Hunt.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum LEVELS
{
	MAIN_MENU		= 0,
	SCAVENGER_HUNT 	= 1,
	SPACE_INVADERS 	= 2,
	HIGH_SCORES 	= 3,
	REGISTRATION	= 4,
}

public class MainGameManager : MonoBehaviour 
{
	public GameObject welcome;

	// Menu Buttons
	public GameObject playMenu;
	public GameObject play;
	public GameObject explore;
	public GameObject about;
	public GameObject quit;
	public GameObject highScores;
	public GameObject continueMenu;
	public GameObject continueAs;

	// Use this for initialization
	void Start () 
	{
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
		}

		// SimplePlayerController player = GameObject.Find("Player").GetComponent<SimplePlayerController>();
		// player.ResetPlayer();
		// if(!player) {
		// }

		/******** DEBUG *********/
		// PlayerPrefs.DeleteAll();

		if(PlayerPrefs.HasKey("PlayerEmail")) {
			ToggleContinue(true);
		}
		else {
			// playMenu.SetActive(true);
			// continueMenu.SetActive(false);
			ShowMainMenuElements();
		}
	}

	void ToggleContinue(bool status)
	{
		if(status)
		{
			continueMenu.SetActive(status);
			playMenu.SetActive(!status);
			welcome.GetComponent<TMP_Text>().text = "Continue as " + PlayerPrefs.GetString("PlayerName") + "?";			
		}
		else {
			ShowMainMenuElements();
		}
	}

	public void ShowMainMenuElements()
	{
		playMenu.SetActive(true);
		continueMenu.SetActive(false);
	}

	public void LoadRegistration()
	{
		SceneManager.LoadScene((int)LEVELS.REGISTRATION);
	}

	public void LoadScavengerHunt()
	{
		if(!PlayerPrefs.HasKey("PlayerEmail")) {
			LoadRegistration();
		}
		else {
			SceneManager.LoadScene((int)LEVELS.SCAVENGER_HUNT);
		}

	}

	public void LoadSpaceInvaders()
	{
		if(!PlayerPrefs.HasKey("PlayerEmail")) {

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

	public void PennTeachIn()
	{
		Application.OpenURL("http://www.upenn.edu/teachin/");
	}

	public GameObject warning;
	public void QuitWarning()
	{	
		warning.SetActive(true);
		continueMenu.SetActive(false);
		playMenu.SetActive(false);
	}

	public void CloseWarning()
	{
		warning.SetActive(false);
		playMenu.SetActive(true);
	}

	public void Quit()
	{
		Application.Quit();
	}
}

public struct PlayerScore 
{
	public string username;
	public int score;
	public string phone;
	public string email;

	public PlayerScore(string _username, int _score, string _phone, string _email)
	{
		username = _username;
		score = _score;
		phone = _phone;
		email = _email;
	}
}
