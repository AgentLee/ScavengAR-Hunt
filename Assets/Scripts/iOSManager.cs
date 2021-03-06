﻿/*
 * This script is used to maintain the state of the game.
 * It should have references to Space InvadARs and the ScavengAR Hunt.
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class iOSManager : MonoBehaviour 
{
	// March 20, 2018
	// 6pm - 10pm
	const int EST 			= 4;
	const int releaseMonth 	= 3;
	const int releaseDay 	= 20;
	const int releaseYear 	= 2018;
	const int releaseHour 	= 18;
	const int releaseMinute = 00;
	const int endHour		= 22;
	const int endMinute		= 15;

	// User system date time
	int month 	= DateTime.Now.Month;
	int day 	= DateTime.Now.Day;
	int year 	= DateTime.Now.Year;
	int hour	= Int32.Parse(DateTime.UtcNow.Hour.ToString()) - EST;
	int minute	= DateTime.UtcNow.Minute;

	public GameObject welcome;

	// Menu Buttons
	public GameObject playMenu;
	public GameObject play;
	public GameObject explore;
	public GameObject about;
	public GameObject continueMenu;
	public GameObject continueAs;
	public GameObject warning;

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

		// playMenu = iosMenu;

		/******** DEBUG *********/
		// PlayerPrefs.DeleteAll();

		// If the player email is recorded, then they played the game before.
		// Ask if they want to continue as that player.
		if(PlayerPrefs.HasKey("PlayerEmail")) {
			ToggleContinue(true);
		}
		else {
			// They haven't played the game before so show the main menu.
			ShowMainMenuElements();
		}
	}

	// Checks user's time and enables scavenger hunt
	private void EnableScavengAR()
	{
		if(	month != releaseMonth || day != releaseDay || year != releaseYear ||
			hour != releaseHour || minute != releaseMinute) {
			explore.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Hunt Begins\nMarch 20, 2018!";
			explore.transform.Find("Text").GetComponent<TextMeshProUGUI>().enableAutoSizing = true;
			explore.GetComponent<Button>().interactable = false;
		}
	}

	// ----------------------------------------
	// Button Events --------------------------
	// ----------------------------------------

	public GameObject howToPlay;
	public void OpenHowToPlay()
	{
		playMenu.SetActive(false);
		howToPlay.SetActive(true);
	}
	
	public void CloseHowToPlay()
	{
		playMenu.SetActive(true);
		howToPlay.SetActive(false);
	}

	public GameObject aboutDesc;
	public void OpenAbout()
	{
		playMenu.SetActive(false);
		aboutDesc.SetActive(true);
	}

	public void CloseAbout()
	{
		playMenu.SetActive(true);
		aboutDesc.SetActive(false);
	}

	// Displays user name and asks if they want to continue as that user.
	// If no, then they can reregister under a different ename.
	// If they happen to register again with the same email, nothing happens.
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

	// Shows the play options, leaderboard, (quit)
	public void ShowMainMenuElements()
	{
		playMenu.SetActive(true);
		continueMenu.SetActive(false);
	}

	// Warns the user before quitting
	// Only active on android devices
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

	// Closes the application.
	// Only active on android devices	
	public void Quit()
	{
		Application.Quit();
	}

	// ----------------------------------------
	// Scene loaders --------------------------
	// ----------------------------------------

	public void LoadRegistration()
	{
		SceneManager.LoadScene((int)LEVELS.REGISTRATION);
	}

	public void LoadScavengerHunt()
	{
		// If the player hasn't played before, they register first.
		if(!PlayerPrefs.HasKey("PlayerEmail")) {
			LoadRegistration();
		}
		else {
			SceneManager.LoadScene((int)LEVELS.SCAVENGER_HUNT);
		}
	}

	public void LoadSpaceInvaders()
	{
		// If the player hasn't played before, they register first.
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
}